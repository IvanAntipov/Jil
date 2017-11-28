namespace JilFork.Deserialize
{
    delegate T StringThunkDelegate<T>(ref ThunkReader writer, int depth);

    struct ThunkReader
    {
        string Value;
        int Index;

        public int Position { get { return Index + 1; } }

        int IndexOrLen
        {
            get
            {
                if (Index > Value.Length)
                {
                    return Value.Length;
                }
                return Index;
            }
        }

        public ThunkReader(string val) : this()
        {
            Value = val;
            Index = -1;
        }

        public int Peek()
        {
            var ix = Index + 1;
            if(ix >= Value.Length)
            {
                return -1;
            }

            return Value[ix];
        }

        public int Read()
        {
            Index++;
            if(Index >= Value.Length)
            {
                return -1;
            }

            return Value[Index];
        }
    }
}
