using System;
using System.Collections.Generic;
using System.Text;

namespace Meowv.Blog.ToolKits.Base.Paged
{
    public class ListResult<T> : IListResult<T>
    {
        IReadOnlyList<T> item;
        public IReadOnlyList<T> Item
        {
            get => item ?? (item = new List<T>());

            set => item = value;
        }
        public ListResult()
        {

        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ListResult(IReadOnlyList<T> item)
        {
            this.item = item;
        }
    }
}
