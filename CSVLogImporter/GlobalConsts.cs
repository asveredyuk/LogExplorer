using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVLogImporter
{
    class GlobalConsts
    {
        public const long MEM_LIMIT = 1073741824;
        public const char CSV_SPLITTER = ',';

        //todo: think about that, what if problems occur here
        public const char DATA_SPLITTER = ',';
        public const char META_SPLITTER = ',';


        public const char META_COMPILED_PREFIX = '_';
        public const char META_COMPILED_MULTI = '|';


    }
}
