using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDataAccess.Models
{
    internal class WordModel
    {
        public Guid Id { get; set; }   
        public string InEnglish { get; set; }
        public string InRussian { get; set; }
        public string Example { get; set; }

        public WordModel(string inEnglish, string inRussian, string example)
        {
            Id = Guid.NewGuid();
            InEnglish = inEnglish;
            InRussian = inRussian;
            Example = example;
        }
    }
}
