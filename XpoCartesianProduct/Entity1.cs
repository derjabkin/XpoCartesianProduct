using DevExpress.Xpo;

namespace XpoCartesianProduct
{
    public class Entity1 : XPObject
    {

        private string name;

        public Entity1(Session session) : base(session)
        {
        }

        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }
    }

}
