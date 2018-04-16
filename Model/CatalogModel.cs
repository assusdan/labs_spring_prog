using Labs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs
{
    class CatalogModel
    {

        public IService ModelService {private get; set; }

        private IList<Track> tracks = new List<Track>();


        public CatalogModel(IList<Track> oldTrackList)
        {
            tracks = oldTrackList;
            ModelService = ServicesManager.Instance.GetService(ServiceTypes.Model);
        }

        public CatalogModel(Track track) : this(new List<Track>() { track })
        {
        }

        public CatalogModel() : this(new List<Track>())
        {
        }

        public void SearchAll()
        {

            // if predicate always returns true then list won't cut down
            printTracksByPredicate(anything => true);

        }

        public void SearchByAuthorName(String authorName)
        {

            if (authorName == null)
            {
                authorName = Track.UnknownAuthor;
            }

            printTracksByPredicate(track => track.AuthorName.Equals(authorName));

        }

        public void SearchByTrackName(String trackName)
        {

            if (trackName == null)
            {
                throw new Exception("Track name can't be null!");
            }

            printTracksByPredicate(track => track.TrackName.Equals(trackName));

        }

        private void printTracksByPredicate(Func<Track, bool> predicate)
        {

            List<Track> foundTracks = new List<Track>(tracks.Where(predicate));

            if (foundTracks.Count() > 0)
            {
                foundTracks.ForEach(track => ModelService.Send(new ServiceMessage(typeof(PrintRequest), track.FullName)));
            }
            else
            {
                ModelService.Send(new ServiceMessage(typeof(PrintRequest), "No one track was found"));
            }
        }

        public void Add(Track track)
        {
            tracks.Add(track);
        }

        public void Add(String authorName, String trackName)
        {
            tracks.Add(new Track(authorName, trackName));
        }

        public void Remove(String authorName, String trackName)
        {
            tracks.Remove(new Track(authorName, trackName));
        }


    }
}
