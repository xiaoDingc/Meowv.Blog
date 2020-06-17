using Meowv.Blog.Domain.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Meowv.Blog.Application.Contracts.Wallpaper
{
    /// <summary>
    /// 壁纸job
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WallpaperJobItem<T>
    {
        /// <summary>
        /// <see cref="Result"/>
        /// </summary>
        public T Result { get; set; }


        /// <summary>
        ///  类型
        /// </summary>
        public WallpaperEnum Type { get; set; }
    }
}
