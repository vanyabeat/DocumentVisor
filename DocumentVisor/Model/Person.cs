using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentVisor.Model
{
    public class Person : IDataField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Phone { get; set; }
        public int TypeId { get; set; }
        public virtual PersonType Type { get; set; }
        [NotMapped]
        public PersonType PersonType
        {
            get
            {
                return DataWorker.GetPersonTypeById(TypeId);
            }
        }
        public override string ToString()
        {
            return $"{Type} {Name} ({Phone})";
        }
    }
}
