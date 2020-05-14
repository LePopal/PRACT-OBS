using System;
using System.Collections.Generic;
using System.Text;

namespace PRACT_OBS.Classes.Data
{
    public class Queries
    {
		public const string QRY_ON_AIR = @"select 
											h.ID,
											h.created_at, 
											c.FolderPath, 
											c.Title as TrackTitle, 
											a.Name as ArtistName,
											c.ImagePath,
											c.BPM,
											c.Rating,
											c.ReleaseYear,
											c.ReleaseDate,
											c.Length,
											c.ColorID,
											c.Commnt as TrackComment,
											co.Commnt as ColorName,
											al.Name as AlbumName,
											la.Name as LabelName,
											ge.Name as GenreName,
											k.ScaleName as KeyName,
											rmx.Name as RemixerName,
											c.DeliveryComment as Message
										from djmdSongHistory as h
										join djmdContent as c on h.ContentID = c.ID
										left join djmdColor as co on c.ColorID=co.id
										left join djmdArtist as a on c.ArtistID = a.ID
										left join djmdArtist as rmx on c.RemixerID = rmx.ID
										left join djmdAlbum as al on c.AlbumID = al.ID
										left join djmdLabel as la on c.LabelID = la.ID
										left join djmdGenre as ge on c.GenreID = ge.ID
										left join djmdKey as k on c.KeyID=k.ID
										order by h.created_at desc
										limit 2";
	}
}
