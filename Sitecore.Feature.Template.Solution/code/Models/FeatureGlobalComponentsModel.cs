using Sitecore.Data.Items;
using System.Collections.Generic;

namespace Sitecore.Feature.GlobalComponentLibrary.Models
{
    public class Site
    {
        public Item SiteItem { get; set; }

        public Item SiteHomeItem { get; set; }

        public Site(Item siteItem, Item siteHomeItem)
        {
            this.SiteItem = siteItem;
            this.SiteHomeItem = siteHomeItem;
        }
    }

    public class MarqueeItem : MarqueeBaseItem
    {
        public string CTAButton1Text { get; set; } = string.Empty;
        public string CTAButton1Link { get; set; } = string.Empty;
        public string CTAButton2Text { get; set; } = string.Empty;
        public string CTAButton2Link { get; set; } = string.Empty;

        public MarqueeItem()  { }
    }

    public abstract class MarqueeBaseItem
    {
        public string Headline { get; set; } = string.Empty;
        public string SubHeadline { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string BackgroundImageAltText { get; set; } = string.Empty;
        public string BackgroundImageUrl { get; set; } = string.Empty;
        public string BackgroundImageMobileUrl { get; set; } = string.Empty;
        public bool OverrideBackgroundWithColor { get; set; }
        public string BackgroundColorCSS { get; set; } = string.Empty;
        public bool SwapPromoPosition { get; set; }
        public string MarqueeLeftColumnCSSSize { get; set; } = string.Empty;
        public string MarqueeRightColumnCSSSize { get; set; } = string.Empty;
    }

    public class CalloutListItem
    {
        public string Title { get; set; } = string.Empty;

        public string BodyContent { get; set; } = string.Empty;

        public string BodyRowColorCSS { get; set; } = string.Empty;

        public List<CTALinkItem> CTACardItems = new List<CTALinkItem>();
    }


    public class CTALinkItem : NavigationLinkItem
    {
        public string CTAStyledClasses { get; set; } = string.Empty;
        public string CTASubHeadline { get; set; } = string.Empty;
        public string CTALinkModalAttributes { get; set; } = string.Empty;

        public CTALinkItem() { }
    }

    public class IconNavigationLinkItem : NavigationLinkItem
    {
        public string IconImageUrl { get; set; } = string.Empty;

        public IconNavigationLinkItem() { }
    }

    public class NavigationLinkItem
    {
        public string LinkText { get; set; } = string.Empty;
        public string LinkUrl { get; set; } = string.Empty;

        public NavigationLinkItem() { }
    }


    public class InformationCardCollection
    {
        public string Headline { get; set; } = string.Empty;

        public List<InformationCard> InfoCards = new List<InformationCard>();

        public InformationCardCollection() { }
    }

    public class InformationCard
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Theme { get; set; } = string.Empty;
        public string CardIconImage { get; set; } = string.Empty;
        public string CardLargeImage { get; set; } = string.Empty;
        public string IconImageUrl { get; set; } = string.Empty;
        public string IconImageLinkText { get; set; } = string.Empty;
        public string IconImageLinkUrl { get; set; } = string.Empty;
        public List<InformationCard> RelatedCards = new List<InformationCard>();

        public InformationCard() { }
    }

    public class InformationMediaCollection
    {
        public string Headline { get; set; } = string.Empty;
        public string BackgroundImage { get; set; } = string.Empty;
        public List<VideoOrImageLayerItem> MediaItems = new List<VideoOrImageLayerItem>();
        public string ReferencesBlockContent { get; set; } = string.Empty;

        public InformationMediaCollection() { }
    }    

    public class ImageLayerItem : MediaItemBase
    {
        public int Index { get; set; }

        public ImageLayerItem() { }
    }

    public class VideoOrImageLayerItem : MediaItemBase
    {
        public string VideoHashId { get; set; } = string.Empty;
        public string Runtime { get; set; } = string.Empty;

        public VideoOrImageLayerItem() { }
    }

    public class MediaItemBase
    {
        public string Title { get; set; } = string.Empty;
        public string SubTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
        public string MediaItemAltText { get; set; } = string.Empty;
        public string CardColumnCSS { get; set; } = string.Empty;

        public MediaItemBase() { }
    }

    public class DownloadItem
    {
        public string ImageAltText { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string DownloadAssetUrl { get; set; } = string.Empty;
        public string DownloadButtonText { get; set; } = string.Empty;

        public DownloadItem() { }
    }
}