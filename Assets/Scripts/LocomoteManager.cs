using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class LocomoteManager : MonoBehaviour
{
    public enum MoveStyleType { HeadRelative, HandRelatetive }
    public enum TurnStyleType { Snap, Contonuous }

    [Header("XR")]
    [SerializeField] XROrigin XROrigin;
    [SerializeField] XRBaseController Controller_Left;
    [SerializeField] XRBaseController Controller_Right;

    [Header("Locomotion")]
    [SerializeField] ContinuousMoveProviderBase Provider_ContinuousMove;
    [SerializeField] ContinuousTurnProviderBase Provider_ContinuousTurn;
    [SerializeField] SnapTurnProviderBase Provider_SnapTurn;
    [SerializeField] TeleportationProvider Provider_Teleportation;

    [Header("Property")]
    [SerializeField] MoveStyleType _leftHandMoveStyle;
    [SerializeField] TurnStyleType _rightHandTurnStyle;
    [SerializeField, Range(0.0f, 5.0f)] float _moveSpeed; //float에도 제약 걸 수 있음.(Range)
    [SerializeField] bool _isEnableStrafe;
    [SerializeField] bool _isUseGravity;
    [SerializeField] bool _isEnableFly;
    [SerializeField, Range(0.0f, 180.0f)] float _turnSpeed;
    [SerializeField] bool _isEnableTurnAround;
    [SerializeField, Range(0.0f, 90.0f)] float _snapTurnAmount;

    //public MoveStyleType GetLeftHandMoveStyle()
    //{
    //    return _leftHandMoveStyle;
    //}

    //public void SetMoveStyle(MoveStyleType moveStyle)
    //{
    //    _leftHandMoveStyle = moveStyle;
    //}

    //gettersetter중에 상태 변화 가능.

    public MoveStyleType MoveStyle
    {
        get { return _leftHandMoveStyle; }
        set
        {
            _leftHandMoveStyle = value;

            switch(_leftHandMoveStyle)
            {
                case MoveStyleType.HeadRelative:
                    Provider_ContinuousMove.forwardSource = XROrigin.Camera.transform;
                    break;

                case MoveStyleType.HandRelatetive:
                    Provider_ContinuousMove.forwardSource = Controller_Left.transform;
                    break;
            }
        }
    }

    public TurnStyleType TurnStyle
    {
        get { return _rightHandTurnStyle; }
        set
        {
            _rightHandTurnStyle = value;

            bool isEnable = (_rightHandTurnStyle == TurnStyleType.Snap ? true : false);
            switch(_rightHandTurnStyle)
            {
                case TurnStyleType.Snap:
                    Provider_ContinuousTurn.enabled = !isEnable;
                    Provider_SnapTurn.enabled = isEnable;
                    break;

                case TurnStyleType.Contonuous:
                    Provider_ContinuousTurn.enabled = isEnable;
                    Provider_SnapTurn.enabled = !isEnable;
                    break;
            }
        }
    }

    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set
        {
            _moveSpeed = value;
            Provider_ContinuousMove.moveSpeed = _moveSpeed;
        }
    }

    public bool IsEnableStrafe
    {
        get { return _isEnableStrafe; }
        set
        {
            _isEnableStrafe = value;
            Provider_ContinuousMove.enableStrafe = _isEnableStrafe;
        }
    }

    public bool IsUseGravity
    {
        get { return _isUseGravity; }
        set
        {
            _isUseGravity = value;
            Provider_ContinuousMove.useGravity = _isUseGravity;
        }
    }
    public bool IsEnableFly
    {
        get { return _isEnableFly; }
        set
        {
            _isEnableFly = value;
            Provider_ContinuousMove.enableFly = _isEnableFly;
        }
    }

    public float TurnSpeed
    {
        get { return _turnSpeed; }
        set
        {
            _turnSpeed = value;
            Provider_ContinuousTurn.turnSpeed = _turnSpeed;
        }
    }

    public bool EnableTurnAround
    {
        get { return _isEnableTurnAround; }
        set
        {
            _isEnableTurnAround = value;
            Provider_SnapTurn.enableTurnAround = _isEnableTurnAround;
        }
    }

    public float SnapTurnAmount
    {
        get { return _snapTurnAmount; }
        set
        {
            _snapTurnAmount = value;
            Provider_SnapTurn.turnAmount = _snapTurnAmount;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }

    public void Init()
    {
        var aa = MoveSpeed;

        MoveStyle = _leftHandMoveStyle;
        TurnStyle = _rightHandTurnStyle;
        MoveSpeed = _moveSpeed;
        IsEnableStrafe = _isEnableStrafe;
        IsUseGravity = _isUseGravity;
        IsEnableFly = _isEnableFly;
        TurnSpeed = _turnSpeed;
        EnableTurnAround = _isEnableTurnAround;
        SnapTurnAmount = _snapTurnAmount;
    }
}
