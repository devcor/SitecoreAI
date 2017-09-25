Sitecore AI
=================
Sitecore stores usefull information about user interactions and the visitors of our sites. Nevertheless, it is necessary a person to analize the data and to clasify what kind of user has visited the site such a new potential client or more important clients for us.

Allowing sitecore to automatically indentify visitors could be possible through Machine Learning processing which this project was made for. On sitecore experience profile we can configure some parameters (labels) that will be analyzed through an AI processor service to clasify or fill the percentage between 0 and 100 of each user accomplish with each label. In that way, next time we enter to the experience profile dashboard we are going to see what kind of user we have in a new column of the table.

About
-
Sitecore AI is a tool that uses Machine Learning advantages to:
- Automatically clasify visitors according to our conditions
- Identify posible clients visiting our site
- Integrate clasification data in Sitecore Experience Extractor to take advantage of its benefits

For further information about [Sitecore Experience Extractor](https://github.com/Sitecore/experience-extractor) please follow the [link](https://github.com/Sitecore/experience-extractor).

Compatibility
-
Sitecore AI has been tested with Sitecore XP 8.2 rev. 170614. Also this package requires access to Sitecore Analytics and Mongo DB database, and it's currently not compatible with Sitecore xDB Cloud Service.

Installation
-
Download and install [SitecoreAI-1.0.0.zip](https://github.com/devcor/SitecoreAI/raw/master/SitecoreAI.Website/Package_Builder/SitecoreAI-1.0.0.zip) on the server(s) where Sitecore AI will be available. Please make sure you have installed [Sitecore Experience Extractor](https://github.com/Sitecore/experience-extractor) before proceding. If Sitecore Experiences extractor is not installed, you won't be able to extract AI information from the system but information will remain in your Sitecore instance.

Furthermore, when you install Sitecore AI package first, and then you install Sitecore Experience Extractor all AI preccess will be working but Sitecore AI information will not be available for using on Sitecore Experience Extractor. To make it available you must install again the Sitecore AI package and overwride the previous one.