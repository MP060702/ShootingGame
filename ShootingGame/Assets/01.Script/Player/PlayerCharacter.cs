using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    #region Movement
    private Vector2 _moveInput;
    public float MoveSpeed;
    #endregion

    #region Skills
    [HideInInspector] public Dictionary<EnumTypes.PlayerSkill, BaseSkill> Skills;
    [SerializeField] private GameObject[] _skillPrefabs;
    #endregion

    #region Invincibility
    private bool invincibility;
    private Coroutine invincibilityCorountine;
    private const double InvincibilityDurationInSeconds = 3;

    public bool Invincibility
    {
        get { return invincibility; } 
        set { invincibility = value; }
    }
    #endregion

    #region Weapon
    public int CurrentWeaponLevel = 0;
    public int MaxWeaponLevel = 4;
    #endregion

    #region AddOn
    public Transform[] AddOnTransform;
    public GameObject AddOnPrefab;
    [HideInInspector] public int MaxAddOnCount = 2;
    #endregion


    public void DeadProcess()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    void Start()
    {
        InitializeSkills();
    }

    private void Update()
    {
        UpdateMovement();
        UpdateSkillInput();
    }

    public void InitSkillCoolDown()
    {
        foreach (var skill in Skills.Values) 
        { 
            skill.InitCoolDown();
        }
    }

    private void UpdateMovement()
    {
        _moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Translate(new Vector3(_moveInput.x, _moveInput.y, 0f) * (MoveSpeed * Time.deltaTime));
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if(pos.x < 0f) pos.x = 0f;
        if(pos.x > 1f) pos.x = 1f;
        if(pos.y < 0f) pos.y = 0f;
        if(pos.y > 1f) pos.y = 1f;

        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    private void UpdateSkillInput()
    {
        if (Input.GetKey(KeyCode.Z)) ActivateSkill(EnumTypes.PlayerSkill.Primary);
        if (Input.GetKeyDown(KeyCode.X)) ActivateSkill(EnumTypes.PlayerSkill.Repair);
        if (Input.GetKeyDown(KeyCode.C)) ActivateSkill(EnumTypes.PlayerSkill.Bomb);
        if (Input.GetKeyDown(KeyCode.V)) ActivateSkill(EnumTypes.PlayerSkill.Shield);
    }

    private void InitializeSkills()
    {
        Skills = new Dictionary<EnumTypes.PlayerSkill, BaseSkill>();

        for(int i = 0; i < _skillPrefabs.Length; i++)
        {
            AddSkill((EnumTypes.PlayerSkill)i, _skillPrefabs[i]);
        }

        GameInstance.instance.CurrentPlayerWeoponLevel = CurrentWeaponLevel;
    }

    private void AddSkill(EnumTypes.PlayerSkill skillType, GameObject prefab)
    {
        GameObject skillObject = Instantiate(prefab, transform.position, transform.rotation);
        skillObject.transform.parent = this.transform;

        if(skillObject != null )
        {
            BaseSkill skillComponent = skillObject.GetComponent<BaseSkill>();
            Skills.Add(skillType, skillComponent);
        }

    }

    private void ActivateSkill(EnumTypes.PlayerSkill skillType)
    {
        if (Skills.ContainsKey(skillType))
        {
            if (Skills[skillType].isAvailable())
            {
                Skills[skillType].Active();
            }
        }
    }

    public void SetInvincibility(bool invin)
    {
        if (invin)
        {
            if(invincibilityCorountine != null)
            {
                StartCoroutine(InvincibilityCorountine());
            }
            invincibilityCorountine = StartCoroutine(InvincibilityCorountine());
        }
    }

    private IEnumerator InvincibilityCorountine()
    {
        Invincibility = true;
        SpriteRenderer spriteRenRenderer = GetComponent<SpriteRenderer>();

        float invincibilityDuraction = 3f;
        spriteRenRenderer.color = new Color(1, 1, 1, 0.5f);

        yield return new WaitForSeconds(invincibilityDuraction);

        Invincibility = false;
        spriteRenRenderer.color = new Color(1, 1, 1, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            BaseItem item = collision.gameObject.GetComponent<BaseItem>();
            
            item.OnGetItem(this);
            Destroy(collision.gameObject);
        }
    }
}
