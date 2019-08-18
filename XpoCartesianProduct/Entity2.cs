using DevExpress.Xpo;

namespace XpoCartesianProduct
{
    public class Entity2 : XPObject
    {

        private string name;

        public Entity2(Session session) : base(session)
        {
        }

        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }
    }

}
