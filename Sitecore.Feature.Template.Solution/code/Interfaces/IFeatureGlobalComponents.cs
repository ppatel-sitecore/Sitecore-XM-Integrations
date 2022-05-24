using Sitecore.Data;
using Sitecore.Feature.GlobalComponentLibrary.Models;

namespace Sitecore.Feature.GlobalComponentLibrary.Interfaces
{
	public interface IFeatureGlobalComponents
	{
		MarqueeItem GetMarqueeContentItem(ID itemId, bool isLargeMarqueeItem = false);

		CalloutListItem GetCalloutListItem(ID itemId);

		InformationCardCollection GetInfoCardCollectionItem(ID itemId);

		InformationCard GetInfoCard(ID itemId);        

		InformationMediaCollection GetInformationMediaCollection(ID itemId);

		ImageLayerItem GetImageLayerItem(ID itemId);

		DownloadItem GetDownloadItem(ID itemId);
	}
}