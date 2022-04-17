namespace Code.ContextSystem
{
    public abstract class Context
    {
        #region Properties
        public bool		CanExit 	{ get; set; }
        public bool 	HasEntered 	{ get; set; }
        #endregion
		
        #region Methods
        public abstract void 	Enter();
        public abstract void 	Exit();

        public abstract void 	SetCanExitAsSoonAsPossible();
        public virtual void 	Update() {}
        #endregion
    }
}