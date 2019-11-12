using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace MyEvents.Client.Organizer.ViewModel.FakeViewModel
{
    /// <summary>
    /// Class to get fake data design time.
    /// </summary>
    public static class FakeDataHelper
    {
        /// <summary>
        /// Gets a list of fake events.
        /// </summary>
        /// <param name="numberOfEvents"></param>
        /// <param name="groupNumber"></param>
        /// <returns></returns>
        public static List<EventDefinition> GetFakeEvents(int numberOfEvents, int groupNumber)
        {
            var eventList = new List<EventDefinition>();

            for (int i = 0; i < numberOfEvents; i++)
            {
                eventList.Add(GetFakeEventWithSessions(groupNumber));
            }
            return eventList;
        }

        /// <summary>
        /// Gets a fake event.
        /// </summary>
        /// <param name="groupNumber"></param>
        /// <returns></returns>
        public static EventDefinition GetFakeEvent(int groupNumber)
        {
            return new EventDefinition()
            {
                Name = "event name",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                Address = "address",
                City = "Seattle",
                Date = DateTime.Now,
                Description = "event description",
                Likes = 5,
                Logo = GetFakeEventLogo(),
                GroupNumber = groupNumber
            };
        }

        /// <summary>
        /// Gets a fake event logo.
        /// </summary>
        /// <returns></returns>
        public static byte[] GetFakeEventLogo()
        {
            string imageAsBytes = "iVBORw0KGgoAAAANSUhEUgAAAGQAAABkEAYAAAAgckkXAAAACXBIWXMAAA7DAAAOwwHHb6hkAAAKT2lDQ1BQaG90b3Nob3AgSUNDIHByb2ZpbGUAAHjanVNnVFPpFj333vRCS4iAlEtvUhUIIFJCi4AUkSYqIQkQSoghodkVUcERRUUEG8igiAOOjoCMFVEsDIoK2AfkIaKOg6OIisr74Xuja9a89+bN/rXXPues852zzwfACAyWSDNRNYAMqUIeEeCDx8TG4eQuQIEKJHAAEAizZCFz/SMBAPh+PDwrIsAHvgABeNMLCADATZvAMByH/w/qQplcAYCEAcB0kThLCIAUAEB6jkKmAEBGAYCdmCZTAKAEAGDLY2LjAFAtAGAnf+bTAICd+Jl7AQBblCEVAaCRACATZYhEAGg7AKzPVopFAFgwABRmS8Q5ANgtADBJV2ZIALC3AMDOEAuyAAgMADBRiIUpAAR7AGDIIyN4AISZABRG8lc88SuuEOcqAAB4mbI8uSQ5RYFbCC1xB1dXLh4ozkkXKxQ2YQJhmkAuwnmZGTKBNA/g88wAAKCRFRHgg/P9eM4Ors7ONo62Dl8t6r8G/yJiYuP+5c+rcEAAAOF0ftH+LC+zGoA7BoBt/qIl7gRoXgugdfeLZrIPQLUAoOnaV/Nw+H48PEWhkLnZ2eXk5NhKxEJbYcpXff5nwl/AV/1s+X48/Pf14L7iJIEyXYFHBPjgwsz0TKUcz5IJhGLc5o9H/LcL//wd0yLESWK5WCoU41EScY5EmozzMqUiiUKSKcUl0v9k4t8s+wM+3zUAsGo+AXuRLahdYwP2SycQWHTA4vcAAPK7b8HUKAgDgGiD4c93/+8//UegJQCAZkmScQAAXkQkLlTKsz/HCAAARKCBKrBBG/TBGCzABhzBBdzBC/xgNoRCJMTCQhBCCmSAHHJgKayCQiiGzbAdKmAv1EAdNMBRaIaTcA4uwlW4Dj1wD/phCJ7BKLyBCQRByAgTYSHaiAFiilgjjggXmYX4IcFIBBKLJCDJiBRRIkuRNUgxUopUIFVIHfI9cgI5h1xGupE7yAAygvyGvEcxlIGyUT3UDLVDuag3GoRGogvQZHQxmo8WoJvQcrQaPYw2oefQq2gP2o8+Q8cwwOgYBzPEbDAuxsNCsTgsCZNjy7EirAyrxhqwVqwDu4n1Y8+xdwQSgUXACTYEd0IgYR5BSFhMWE7YSKggHCQ0EdoJNwkDhFHCJyKTqEu0JroR+cQYYjIxh1hILCPWEo8TLxB7iEPENyQSiUMyJ7mQAkmxpFTSEtJG0m5SI+ksqZs0SBojk8naZGuyBzmULCAryIXkneTD5DPkG+Qh8lsKnWJAcaT4U+IoUspqShnlEOU05QZlmDJBVaOaUt2ooVQRNY9aQq2htlKvUYeoEzR1mjnNgxZJS6WtopXTGmgXaPdpr+h0uhHdlR5Ol9BX0svpR+iX6AP0dwwNhhWDx4hnKBmbGAcYZxl3GK+YTKYZ04sZx1QwNzHrmOeZD5lvVVgqtip8FZHKCpVKlSaVGyovVKmqpqreqgtV81XLVI+pXlN9rkZVM1PjqQnUlqtVqp1Q61MbU2epO6iHqmeob1Q/pH5Z/YkGWcNMw09DpFGgsV/jvMYgC2MZs3gsIWsNq4Z1gTXEJrHN2Xx2KruY/R27iz2qqaE5QzNKM1ezUvOUZj8H45hx+Jx0TgnnKKeX836K3hTvKeIpG6Y0TLkxZVxrqpaXllirSKtRq0frvTau7aedpr1Fu1n7gQ5Bx0onXCdHZ4/OBZ3nU9lT3acKpxZNPTr1ri6qa6UbobtEd79up+6Ynr5egJ5Mb6feeb3n+hx9L/1U/W36p/VHDFgGswwkBtsMzhg8xTVxbzwdL8fb8VFDXcNAQ6VhlWGX4YSRudE8o9VGjUYPjGnGXOMk423GbcajJgYmISZLTepN7ppSTbmmKaY7TDtMx83MzaLN1pk1mz0x1zLnm+eb15vft2BaeFostqi2uGVJsuRaplnutrxuhVo5WaVYVVpds0atna0l1rutu6cRp7lOk06rntZnw7Dxtsm2qbcZsOXYBtuutm22fWFnYhdnt8Wuw+6TvZN9un2N/T0HDYfZDqsdWh1+c7RyFDpWOt6azpzuP33F9JbpL2dYzxDP2DPjthPLKcRpnVOb00dnF2e5c4PziIuJS4LLLpc+Lpsbxt3IveRKdPVxXeF60vWdm7Obwu2o26/uNu5p7ofcn8w0nymeWTNz0MPIQ+BR5dE/C5+VMGvfrH5PQ0+BZ7XnIy9jL5FXrdewt6V3qvdh7xc+9j5yn+M+4zw33jLeWV/MN8C3yLfLT8Nvnl+F30N/I/9k/3r/0QCngCUBZwOJgUGBWwL7+Hp8Ib+OPzrbZfay2e1BjKC5QRVBj4KtguXBrSFoyOyQrSH355jOkc5pDoVQfujW0Adh5mGLw34MJ4WHhVeGP45wiFga0TGXNXfR3ENz30T6RJZE3ptnMU85ry1KNSo+qi5qPNo3ujS6P8YuZlnM1VidWElsSxw5LiquNm5svt/87fOH4p3iC+N7F5gvyF1weaHOwvSFpxapLhIsOpZATIhOOJTwQRAqqBaMJfITdyWOCnnCHcJnIi/RNtGI2ENcKh5O8kgqTXqS7JG8NXkkxTOlLOW5hCepkLxMDUzdmzqeFpp2IG0yPTq9MYOSkZBxQqohTZO2Z+pn5mZ2y6xlhbL+xW6Lty8elQfJa7OQrAVZLQq2QqboVFoo1yoHsmdlV2a/zYnKOZarnivN7cyzytuQN5zvn//tEsIS4ZK2pYZLVy0dWOa9rGo5sjxxedsK4xUFK4ZWBqw8uIq2Km3VT6vtV5eufr0mek1rgV7ByoLBtQFr6wtVCuWFfevc1+1dT1gvWd+1YfqGnRs+FYmKrhTbF5cVf9go3HjlG4dvyr+Z3JS0qavEuWTPZtJm6ebeLZ5bDpaql+aXDm4N2dq0Dd9WtO319kXbL5fNKNu7g7ZDuaO/PLi8ZafJzs07P1SkVPRU+lQ27tLdtWHX+G7R7ht7vPY07NXbW7z3/T7JvttVAVVN1WbVZftJ+7P3P66Jqun4lvttXa1ObXHtxwPSA/0HIw6217nU1R3SPVRSj9Yr60cOxx++/p3vdy0NNg1VjZzG4iNwRHnk6fcJ3/ceDTradox7rOEH0x92HWcdL2pCmvKaRptTmvtbYlu6T8w+0dbq3nr8R9sfD5w0PFl5SvNUyWna6YLTk2fyz4ydlZ19fi753GDborZ752PO32oPb++6EHTh0kX/i+c7vDvOXPK4dPKy2+UTV7hXmq86X23qdOo8/pPTT8e7nLuarrlca7nuer21e2b36RueN87d9L158Rb/1tWeOT3dvfN6b/fF9/XfFt1+cif9zsu72Xcn7q28T7xf9EDtQdlD3YfVP1v+3Njv3H9qwHeg89HcR/cGhYPP/pH1jw9DBY+Zj8uGDYbrnjg+OTniP3L96fynQ89kzyaeF/6i/suuFxYvfvjV69fO0ZjRoZfyl5O/bXyl/erA6xmv28bCxh6+yXgzMV70VvvtwXfcdx3vo98PT+R8IH8o/2j5sfVT0Kf7kxmTk/8EA5jz/GMzLdsAAAAgY0hSTQAAeiUAAICDAAD5/wAAgOkAAHUwAADqYAAAOpgAABdvkl/FRgAADFVJREFUeNrsXWlUVdcVfk8FIw4LU4eoC8GhjYkaq8u6jEnq0ERNJA4EFQlGMQTHtjRxyCqoVI1xSJvEoMsBozFKHMOLdaom4rRMmlITFIdYB0ScZwMOCL7++L5D+54PBHyPO+3vz7cO8Hj3nrO/u/c+59x97E6n0+l02gQCgQdUki4QCEQgAi8jIb5H5elbbB3AVS6Dq8a4/b6j0e/TLiGWoOzC+PUGtCIPgqP2guvMAH8VCc6qBU65BL4SMO29fxT+uWd2ughEYEJhtDyB1uh48OBQcI3Ikj99tTf4UAE4vjo42x+CyUoRgQgMLIw2V9GK+Tv49S/BtR3l+6+nzoNvhYNTc8GHZ4K3dwOfHQAB2RwiEIGOPcaI3eBYtv0TffvtK5eBf2wAnjMGfHsUBRMnAhHoQBj9m9JzfAZu9EYFXQa/18bryIylp+lLD/MfCGVHnAhEUJGhVE+0hueBQ/8ADgrX19VmBoLTOUuWmgo+sAd8KRsCyo0RgQi8mXwPAUeNANeMNNbdZNDTOLLINyCUH78XgQh0kHzrBbmz6Ekag/fT0ziOg09mQTg7Y0QgAh0l31rj/k3wcU43J70KXjMWgjkX5v6JKmIyVhCE/Xm0OkeABzCUinicwnBYozcqceHyl2zH0TWksX3OJgKxJJQwxnwL7m7QHMPbaMIdAE2aMcl/UFJiPFYIpQasojBeEWF4QmFgsT5HOsfMybfKMVQoJcIoBreK+4WEWKb0GGpWqij5dkgPlQ8iEHN4DLXAZ9Hk24dpvXSBGTyGWvlWC3y1RRjiQawoCNs+tLpxgauf5BhGEQgGsFJ7tJzzwfYOWIC5f1+62htQwojJAL8mOYbeQywIgyv6tj99B56yiE+2Zfi9/1npam+EUv3+SGEkUBiJ0kM68yAYsDps1V4N/ss/wZ22gYPpQdLG8++a43NrEuBRLk6Trpfk2xQCwUDVy0QrKAA8cgW4TS9w+/6eP92Vm8Ya0LO0m43/9yG3JR+LgmDuZMlQeEy+Db671oQCwQBVboVW2GPglzuDu08C12/OjweU7mtacDflk/7gxuvAi4bh+9Z1gVAKfyvJtyTfusxBMFBdaMjTvgGn1ARHXwY34gs0VSLK93V27qZ88W98QvYBTzjKXGWvJN822/8W+GS6VmcepONF8Dh6iMo3fWwYz4C7MOlsnQahpKpXLJvDs9wbby6P4b67dvhT4PAoPrck+dabB2HOcLdihPHAZaxnSLGcOU40uPd1FiYbZa5ud99d2287+0Fm+/QtkHs6Kej1HAUzkNOaL+WaK/l2313rJ7N6xhCI7bo+LstvOjh0LTipBwxsWCXwMyuNJQzZXWsWgegM1RhaNaUhzT0Nfqs1DK9VqDE8hvvuWrMk3yf4APtkrwhEF3isIXhQX/CMCzDE6ASu14RonXxzNjAJP1VVQlQxBLOsfP9uBSZPmnH9K2itWQVi0M2KvzgK5jql7dkW4Lavw0AXBGMAD8Zqm3yb5dXWvKPozxpPFvOczREPoms8fgQcVY+xfl0+yaf41mPY9oG7tcNPR3K6tneIOYQxgaU/642zag5isu3utbmQOYbrK93TYcBJLGXpmIUn4en93vk+9wW+onUMg0/XhqxBP6ki07b1IhCv4tzb4Hwmb8HfVfB9UQC/YnPcv8B1OBu28BAM4ExBWXMM11DqgQU+gwojr4AhlJ9NUBEh1t5P0eEh3OWbfUzb2wz6CBzNvWMxcTD4jmXMUcy2wLdjoQhDE4H4FR29hQEIZqWuvEyNhZIIjngNPHMyhPLsJHBgO8+eo6UquPY5QzeDL/Al3sa4dB0uEtAmB6nl+cc1R4N/nguu3kqb21a7izn5ZfuYOcRaBwSRwjIwnbib+QUe7BKx2djJdw0/CCOvQExfZ0k6Bsa5iwPFhb7cA9oKReE3DjLP3pvKlXy1u9jJEMp+3VjDeyqCoe4qMXVdhVilhfIoWodeRQh1FYaC3WGsYf0kTIRhMA9SOo9y6gzajRvK0JQHIT04PbtV+sIUHsRdMMGNdOZRjDKML4kwTC4QnYdeOsMmvvpcazY98dfSJyYLsYyZzGuN9wLRPwk3xHQt7UHEoxDc7Fj1JxGGCKREj8IV39bWEMpBvoNf9xruO7+FmKqEWOXwKFovOHoLt/i+SFI7CGLCbDFNEYgXc5STp9AOaWzMYahTDfd1u4mYpIRYPhBMk2BjXv2q3hTGK2KKIhCvgkWzG4KNepD1wPW4/rQ+YooiEC8KQ+UcauXd6OjCTZJZ28QkRSBeEIZKzs2G4BeVR+SrvMvFREUgZRCGWjC0W6TI9TcsQnH+OTFVEYgFPUZpUX8P+mMDBfPEFDFdiwrE2h7jYejFkOvcRPRT0A7pE0t6kAtfyNCUBtmdWd5otfSFb6D5QqGrx1DCsPqmxLIirT/6cUII2mpFXq3QCwznQTyHUloLw7nFtV1gsFmkmSfBl2SrilEFAmEEnNdZ8s2Cc9vGgJfzuIVkPpFzEo01rAFN0c8qV+k1XkxdXwK571kYKlbOq6+v5HvLUvCiughNBvPouY+vUCiDwacNVqRZzXZtmIn+TxSL14dA7t9xFUain4qVNQ6hWGR5J0uRLmF5n9FTwRuLQjwI5UhfCoSGtmSGMYWiMJkLkDc3g5sFigQ0EcjpoRiAwxTE5HydJLMsSbqAu2ZHVoUQTsRx02Cy+ydYonQZWgtbgj/lfamfGw01ef76sWsYp/r7RAoVKpDfh4NbaDz9eJHCXEqP8T5nyVa3guHfLfUsnqtQFoe7CsaoQlE43xZC2V5dJOGKKua8rcwsGvC/wSmFMPArjzwrxerwa2FQS9RzhiHXMNb+DQo3Zr91zcV9ZY5Fe2ADnrMyVjyIKZDDWah1DKFScyiMbG9/kxIKWmt4tNqGOeCfU4zdjy0/4IPmHc46NhWBGBqHmGzPawNetB0GnBPn62/mE5YGtIAhyvL5xk7m3XFpNoQy81wxf5AvAtEVTvOJncTTYkfGw1Dfj2Wu0K2irwjfm8GFxrmfmSOZVwgIA49/AkKJd/OQ93JFILrACR7Ms5jVTWa9A8Pc9ZFertDVo5gtmS+6y0EQSg6r3nfqIgLRBNd2gL9lMelontqZ3Ik5QIJuTcglR1HrJ8lDzBV6NeIKfYMEswrEbRbLrt4z0DjJvMYDa77g4ZxLBsHg0t813LPWZdZr0RvsZwommn9l1FkvyyXp9zS+nDs8hyOFs07zVhlVGJ6FYrYFR8sJ5Kt48G1HxV7GXe7d+pDHOH95hrG86SoKugpFTQ+n8rf5iWKSuhZI+nXwGNaZOrYUnLvQu19bEAJeza0d0TzoJYHVyrdnm73jXZP55FfpWZqKR9EX7E6n0+mpqhRi5rYUSJ8V4FAuxD2dDq5Wxnej7+4Br+Pen+TFMJS0/VYfCB4WSo/Sn0IZtkZylIpA6FOww41HHuJB3J9wPwxF64OXKZS64Olp9AAMxZzFlPwsnAdWu2encSHtbe7u3SVvvHn0KCqZN/ruYRN7kJKfdLan0fLj9OtkHqvchwpsxUMwV3IPz3quW6zuCUMolBd4StXPjSiUtxhyxXABUv1c4GsPUmaBeB5I5UD683jhQFZfP/wCeG03lvGXswcfKfQasRscy7a/JPVahVhlCw1UTv3XleCJP4FTAkUYksxbKsQSSDIvHkQgybxABGJ8ocjKvAhEUAahmHX3sAhE4BWhmH33sAhE4FWhmO1VYJ8hQARi6WTerK8CewvOr0UglhaKWV8FflR8z1eFj4SJQEQoFngV+GHIob1vZb22qRnga1uL+4QsFFoUPICHQnlT1fUyyYLjxaHgY/ScW3l6wLal4AtheGAcb/6w/yQCEaH836bI2IPg6HeNJZS7IeCNF8kTwVvOQwhn55T3P4tABB6EonYPh7MAn9raohfs5KzT5jfB8zdBCDe8fp0iEEEJoVcoz00Zngdu07NiruIyQ6Pc5xki8bWK3bTUjDkQxIFNvr4SEYigBKHUjEQragR49BDfeJSrvcFZ/uB5n4N/cEAI+yK16gcRiECDZD49lMn0VfAyFgvJYui0/zqPowjT+v5FIILyJPMNmMzXoFDcCsc5uW3czqo0GTfpEZgsTx8HvlILQriq29q+IhBBeTxKe7TCONs1gkfqBY0Cz2W1mgw7BJDSAZ+zc3rVaZhaBCIQgRcEY2foVWkSaw4cMsv9iUAEghIgW00EAhGIQFA+/HcAzoFqCuFMDYAAAAAASUVORK5CYII=";
            var bytes = Convert.FromBase64String(imageAsBytes);
            return bytes;
        }

        /// <summary>
        /// Gets a fake event with sessions.
        /// </summary>
        /// <param name="groupNumber"></param>
        /// <returns></returns>
        public static EventDefinition GetFakeEventWithSessions(int groupNumber = 1)
        {
            var eventDefinition = GetFakeEvent(groupNumber);

            var now = DateTime.Now;
            var startTime = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);

            eventDefinition.StartTime = startTime;

            var sessions = new List<Session>();
            for (int i = 0; i < 12; i++)
            {
                var session = GetFakeSession();
                session.StartTime = startTime.AddHours(i);
            }

            eventDefinition.Sessions = sessions;

            return eventDefinition;
        }

        /// <summary>
        /// Gets a fake session.
        /// </summary>
        /// <returns></returns>
        public static Session GetFakeSession()
        {
            return new Session()
            {
                SessionId = 1,
                AttendeesCount = 5,
                Biography = "author biography",
                Description = "sesison description",
                Duration = 60,
                IsFavorite = false,
                RoomNumber = 1,
                Score = 3,
                Speaker = "Orville McDonald",
                StartTime = DateTime.Now,
                TimeZoneOffset = 2,
                Title = "session title",
                TwitterAccount = "@speaker",
                UserScore = 3,
                Comments = new List<Comment>() { 
                    GetComment(),
                    GetComment(),
                    GetComment(),
                    GetComment()
                }
            };
        }

        /// <summary>
        /// Gets a list of event hours.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetEventHours()
        {
            var now = DateTime.Now;
            var startTime = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            var hours = new List<string>();

            for (int i = 0; i < 12; i++)
            {
                hours.Add(startTime.AddHours(i).ToString("hh:mm tt"));
            }
            return hours;
        }

        /// <summary>
        /// Gets a list of fake twitter items.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TwitterItem> GetTwitterItems()
        {
            var twitterItems = new List<TwitterItem>();

            for (int i = 0; i < 10; i++)
            {
                twitterItems.Add(new TwitterItem()
                {
                    Tweet = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud",
                    User = new TwitterUser()
                    {                        
                        Fullname = "Microsoft",
                        Username = "microsoft"
                    }
                });
            }

            return twitterItems;
        }
        
        /// <summary>
        /// Gets a list of fake user details viewmodel.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SessionUserDetailsViewModel> GetFakeUserDetailsViewModels()
        {
            var userDetailViewModels = new List<SessionUserDetailsViewModel>();
            for (int i = 0; i < 5; i++)
            {
                userDetailViewModels.Add(new SessionUserDetailsViewModel()
                {
                    Name = "user name",
                    Rated = true,
                    Score = 4,
                    Bio = "user bio",
                    Photo = string.Format("https://graph.facebook.com/{0}/picture", "100004210809580")
                });
            }
            return userDetailViewModels;
        }

        /// <summary>
        /// Gets a fake comment.
        /// </summary>
        /// <returns></returns>
        public static Comment GetComment()
        {
            return new Comment()
            {
                AddedDateTime = DateTime.Now,
                CommentId = 1,
                Text = "one comment",
                RegisteredUser = new RegisteredUser(),
                RegisteredUserId = 1,
                SessionId = 1
            };
        }
    }
}
