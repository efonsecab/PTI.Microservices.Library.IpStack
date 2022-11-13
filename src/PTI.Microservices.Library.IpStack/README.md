# PTI.Microservices.Library.IpStack

Facilitates the consumption of the APIs in IpStack

**Examples:**

**Note: The examples below are passing null for the logger, if you want to use the logger make sure to pass the parameter with a value other than null**

## Get Ip GeoLocation Info
    CustomHttpClient customHttpClient = new CustomHttpClient(new CustomHttpClientHandler(null));
    IpStackService ipStackService =
        new IpStackService(logger: null, base.IpStackConfiguration, customHttpClient);
    var result = await ipStackService.GetIpGeoLocationInfoAsync(IPAddress.Parse(this.TestIpAddress));