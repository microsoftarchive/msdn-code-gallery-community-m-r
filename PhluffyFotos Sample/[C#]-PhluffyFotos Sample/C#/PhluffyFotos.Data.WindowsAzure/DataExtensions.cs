namespace PhluffyFotos.Data.WindowsAzure
{
    using System.Collections.Generic;

    public static class DataExtensions
    {
        public static IEnumerable<Photo> ToModel(this IEnumerable<PhotoRow> rows)
        {
            foreach (var row in rows)
            {
                yield return row.ToModel();
            }
        }

        public static Photo ToModel(this PhotoRow row)
        {
            return new Photo()
            {
                PhotoId = row.PhotoId,
                AlbumId = row.AlbumId,
                Owner = row.Owner,
                ThumbnailUrl = row.ThumbnailUrl,
                Title = row.Title,
                Url = row.Url,
                Description = row.Description,
                RawTags = row.RawTags
            };
        }

        public static IEnumerable<Album> ToModel(this IEnumerable<AlbumRow> rows)
        {
            foreach (var row in rows)
            {
                yield return row.ToModel();
            }
        }

        public static Album ToModel(this AlbumRow row)
        {
            return new Album()
            {
                AlbumId = row.AlbumId,
                Title = row.Title,
                Owner = row.Owner,
                ThumbnailUrl = row.ThumbnailUrl,
                HasPhotos = row.HasPhotos
            };
        }
    }
}
