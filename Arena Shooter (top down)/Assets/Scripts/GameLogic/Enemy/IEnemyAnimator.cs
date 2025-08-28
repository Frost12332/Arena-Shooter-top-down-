namespace Assets.Scripts.GameLogic.Enemy
{
    public interface IEnemyAnimator
    {
        void PlayMove(float speed);
        void PlayStopMove();
    }
}