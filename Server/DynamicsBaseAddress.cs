﻿namespace Cblx.Dynamics.Explorer;

public static class DynamicsBaseAddress
{
    public static Uri FromResourceUrl(string resourceUrl)
    {
        return new Uri(new Uri(resourceUrl), "api/data/v9.1/");
    }
}
