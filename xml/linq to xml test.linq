<Query Kind="Statements" />

XElement xml = XElement.Parse(
@"<MetaData entityName=""BurstReadingInterval"" commandType=""Select"">
<Keys>
<Key>ReadingId</Key>
<Key>MeterId</Key>
<Key>IntervalDate</Key>
</Keys>
</MetaData>"
);

xml.Attribute("entityName").Dump();
xml.Attribute("commandType").Dump();

var keys = xml.Element("Keys");

foreach(var k in keys.Elements("Key"))
{
	k.Value.Dump();
}