namespace Code.Managers
{
    public interface IUpdatable
    {
        void 	Update();
    }
    
    public interface ILateUpdatable
    {
        void 	LateUpdate();
    }
}