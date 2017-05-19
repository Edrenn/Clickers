using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.Models.Skills
{
    public class SkillsEnum
    {
        public enum SkillsList { Attack, Parade, Feinte };
        
        private SkillsEnum()
        {
            var test = SkillsList.Attack;
        }
        public SkillsEnum _instance;
        public SkillsEnum _Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SkillsEnum();
                }
                return _instance;
            }   
        }
    }
}
