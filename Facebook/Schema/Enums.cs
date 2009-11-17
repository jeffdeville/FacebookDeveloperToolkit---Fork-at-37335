using System.Collections.Generic;
namespace Facebook.Schema
{
    /// <summary>
    /// Facebook Enums TODO: Move all these to an xml as key/value pairs and use Linq to XML or something cleaner
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// Extended Permissions
        /// </summary>
        public enum ExtendedPermissions
        {
            /// <summary>
            /// Empty value
            /// </summary>
            none,
            /// <summary>
            /// Status Update
            /// </summary>
            status_update,
            /// <summary>
            /// Photo Upload
            /// </summary>
            photo_upload,
            /// <summary>
            /// Send email
            /// </summary>
            email,
            /// <summary>
            /// Offline access
            /// </summary>
            offline_access,
            /// <summary>
            /// Create event
            /// </summary>
            create_event,
            /// <summary>
            /// rsvp an event
            /// </summary>
            rsvp_event,
            /// <summary>
            /// send sms
            /// </summary>
            sms,
            /// <summary>
            /// publish a stream
            /// </summary>
            publish_stream,
            /// <summary>
            /// read stream
            /// </summary>
            read_stream,
            /// <summary>
            /// upload video
            /// </summary>
            video_upload,
            /// <summary>
            /// create note
            /// </summary>
            create_note,
            /// <summary>
            /// share item
            /// </summary>
            share_item,
            /// <summary>
            /// manage mailbox
            /// </summary>
            manage_mailbox,
            /// <summary>
            /// read mail
            /// </summary>
            read_mailbox,
        }

        /// <summary>
        /// Represents the type of an object property.
        /// </summary>
        public enum ObjectPropertyType
        {
            /// <summary>
            /// None
            /// </summary>
            None = 0,
            /// <summary>
            /// Integer.
            /// </summary>
            Integer = 1,
            /// <summary>
            /// String with less than 255 characters.
            /// </summary>
            String = 2,
            /// <summary>
            /// Text blob which less than 64kb.
            /// </summary>
            TextBlob = 3
        }

        /// <summary>
        /// Type of data association.
        /// </summary>
        public enum DataAssociationType
        {
            /// <summary>
            /// None
            /// </summary>
            None = 0,

            /// <summary>
            /// One-way association, where reverse lookup is not needed.
            /// </summary>
            OneWay = 1,

            /// <summary>
            /// Two-way symmetric association, where a backward association
            /// (B to A) is always created when a forward association (A to B) is created.
            /// </summary>
            TwoWaySymmetric = 2,

            /// <summary>
            /// Two-way asymmetric association, where a backward association (B to A) has
            /// different meaning than a forward association (A to B).
            /// </summary>
            TwoWayAsymmetric = 3
        }

        /// <summary>
        /// Integration point names
        /// </summary>
        public enum IntegrationPointName
        {
            /// <summary>
            /// Notifications per day
            /// </summary>
            notifications_per_day,
            /// <summary>
            /// requests per day
            /// </summary>
            requests_per_day,
            /// <summary>
            /// emails per day
            /// </summary>
            emails_per_day,
            /// <summary>
            /// emails disable message location
            /// </summary>
            email_disable_message_location
        }
        /// <summary>
        /// Available file types for video and image upload
        /// </summary>
        public enum FileType
        {
            /// <summary>
            /// .asf
            /// </summary>
            asf,
            /// <summary>
            /// .avi
            /// </summary>
            avi,
            /// <summary>
            /// .flv
            /// </summary>
            flv,
            /// <summary>
            /// .mp4
            /// </summary>
            m4v,
            /// <summary>
            /// .mkv
            /// </summary>
            mkv,
            /// <summary>
            /// .mov
            /// </summary>
            mov,
            /// <summary>
            /// .mp4
            /// </summary>
            mp4,
            /// <summary>
            /// .mpe
            /// </summary>
            mpe,
            /// <summary>
            /// .mpeg
            /// </summary>
            mpeg,
            /// <summary>
            /// .mpeg4
            /// </summary>
            mpeg4,
            /// <summary>
            /// .mpg
            /// </summary>
            mpg,
            /// <summary>
            /// .nsv
            /// </summary>
            nsv,
            /// <summary>
            /// .ogm
            /// </summary>
            ogm,
            /// <summary>
            /// .qt
            /// </summary>
            qt,
            /// <summary>
            /// .vob
            /// </summary>
            vob,
            /// <summary>
            /// .wmv
            /// </summary>
            wmv,
            /// <summary>
            /// .bmp
            /// </summary>
            bmp,
            /// <summary>
            /// .gif
            /// </summary>
            gif,
            /// <summary>
            /// .jpg
            /// </summary>
            jpg,
            /// <summary>
            /// .png
            /// </summary>
            png,
            /// <summary>
            /// .psd
            /// </summary>
            psd,
            /// <summary>
            /// .tiff
            /// </summary>
            tiff,
            /// <summary>
            /// .jp2
            /// </summary>
            jp2,
            /// <summary>
            /// .iff
            /// </summary>
            iff,
            /// <summary>
            /// .wbmp
            /// </summary>
            wbmp,
            /// <summary>
            /// .xbm
            /// </summary>
            xbm
        }
    }
}