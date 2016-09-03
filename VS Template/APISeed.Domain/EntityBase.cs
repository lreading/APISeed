namespace $safeprojectname$
{
    /// <summary>
    /// Base class for all entities
    /// </summary>
    public abstract class EntityBase
    {
        private object _id;

        public virtual object Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public dynamic Clone()
        {
            return MemberwiseClone();
        }
    }
}
