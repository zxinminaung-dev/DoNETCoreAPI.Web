namespace DoNETCoreAPI.Web.Utilities.Pagination
{
    public class Pager
    {
        public int TotalRecordCount { get; set; }
        public int RecordPerPage { get; set; }
        public int CurrentPageNumber { get; set; }


        public int[] PageList
        {

            get
            {
                if (TotalRecordCount > 0)
                {
                    int pageCount = 0;
                    int dividend = Convert.ToInt32(TotalRecordCount);
                    int divider = RecordPerPage;
                    if (dividend > 0 && divider > 0)
                    {
                        pageCount = Convert.ToInt32(dividend / divider) + 1;
                    }

                    int[] tmp = new int[pageCount];

                    for (int i = 0; i < pageCount; i++)
                    {
                        tmp[i] = i + 1;
                    }
                    return tmp;
                }
                else
                {
                    int[] tmp = new int[1];
                    tmp[0] = 0;
                    return tmp;
                }
            }
        }

        public List<int> getPrevious(int cnt)
        {
            List<int> previous = new List<int>();
            if (CurrentPageNumber != 0 && CurrentPageNumber != 1)
            {
                int[] pageList = PageList;

                if (pageList.Length > 0)
                {
                    if (pageList[0] != 0)
                    {
                        for (int i = 1; i <= cnt; i++)
                        {
                            int tmpPageNum = CurrentPageNumber - i;
                            int tmpPageListIndex = tmpPageNum - 1;
                            if (tmpPageListIndex >= 0)
                            {
                                if (pageList[tmpPageListIndex] != CurrentPageNumber)
                                {
                                    previous.Insert(0, pageList[tmpPageListIndex]);
                                }
                            }
                        }
                    }
                }
            }
            return previous;
        }

        public List<int> getNexts(int cnt)
        {
            List<int> nexts = new List<int>();
            if (CurrentPageNumber > 0)
            {
                int[] pageList = PageList;

                if (pageList.Length > 0)
                {
                    if (pageList[0] != 0)
                    {
                        for (int i = 1; i <= cnt; i++)
                        {
                            int tmpPageNum = CurrentPageNumber + i;
                            int tmpPageIndex = tmpPageNum - 1;
                            if (tmpPageNum <= pageList.Length)
                            {
                                if (CurrentPageNumber != pageList[tmpPageIndex])
                                {
                                    nexts.Add(pageList[tmpPageIndex]);
                                }
                            }
                        }
                    }
                }
            }
            return nexts;
        }

        public Int64 getSidx()
        {
            int sidx = 0;
            if (CurrentPageNumber > 0 && RecordPerPage > 0)
            {
                sidx = (CurrentPageNumber - 1) * RecordPerPage;
            }
            if (TotalRecordCount > 0 && sidx == 0)
            {
                sidx = 1;
            }
            if (TotalRecordCount > 0 && sidx > 1)
            {
                sidx = sidx + 1;
            }
            return sidx;
        }

        public Int64 getEidx()
        {
            Int64 indx = 0;
            if (RecordPerPage > 0 && TotalRecordCount > 0)
            {
                if (TotalRecordCount > RecordPerPage)
                {
                    Int64 i = (RecordPerPage * CurrentPageNumber);

                    if (i > TotalRecordCount)
                    {

                        Int64 tmp = RecordPerPage - (i - TotalRecordCount);
                        indx = (RecordPerPage * (CurrentPageNumber - 1)) + tmp;

                    }
                    else
                    {
                        indx = i;
                    }

                }
                else
                {
                    indx = TotalRecordCount;
                }

            }
            return indx;
        }

        public Pager()
        {
            TotalRecordCount = 0;
            RecordPerPage = 10;
            CurrentPageNumber = 1;
        }

        public int getFirstPage
        {

            get
            {
                if (TotalRecordCount > 0)
                {
                    int pageCount = 0;
                    int dividend = Convert.ToInt32(TotalRecordCount);
                    int divider = RecordPerPage;
                    if (dividend > 0 && divider > 0)
                    {
                        pageCount = Convert.ToInt32(dividend / divider) + 1;
                    }

                    int[] tmp = new int[pageCount];

                    for (int i = 0; i < pageCount; i++)
                    {
                        tmp[i] = i + 1;
                    }
                    return tmp.First();
                }
                else
                {
                    int[] tmp = new int[1];
                    tmp[0] = 0;
                    return tmp.First();
                }
            }
        }


        public int getLastPage
        {

            get
            {
                if (TotalRecordCount > 0)
                {
                    int pageCount = 0;
                    int dividend = Convert.ToInt32(TotalRecordCount);
                    int divider = RecordPerPage;
                    if (dividend > 0 && divider > 0)
                    {
                        pageCount = Convert.ToInt32(dividend / divider) + 1;
                    }

                    int[] tmp = new int[pageCount];

                    for (int i = 0; i < pageCount; i++)
                    {
                        tmp[i] = i + 1;
                    }
                    return tmp.Last();
                }
                else
                {
                    int[] tmp = new int[1];
                    tmp[0] = 0;
                    return tmp.Last();
                }
            }
        }
    }
}
