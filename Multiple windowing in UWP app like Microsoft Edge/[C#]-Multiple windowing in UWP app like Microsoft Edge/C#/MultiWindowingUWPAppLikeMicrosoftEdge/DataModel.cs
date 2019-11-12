using System;

namespace MultiWindowingUWPAppLikeMicrosoftEdge
{
    class DataModel
    {
        public Guid Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public DataModel()
        {
            this.Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
