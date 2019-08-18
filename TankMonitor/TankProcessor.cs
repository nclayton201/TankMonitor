using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TankMonitor.Data;
using TankMonitor.SiteDb;

namespace TankMonitor
{
    public interface ITankProcessor
    {
        List<Inventory> FetchInventories(int siteId);
        List<Inventory> LatestInventories(List<Inventory> inputInventories);
        List<Tank> FetchTanks(int siteId);
        List<Site> FetchSites();
    }

    public class TankProcessor : ITankProcessor
    {
        private SiteContext _siteContext;

        public TankProcessor(SiteContext context)
        {
            _siteContext = context;
        }

        // Returns list of inventories belonging to input Site ID
        public List<Inventory> FetchInventories(int siteId)
        {
            List<Inventory> fullInventories = _siteContext.Inventories.ToList();
            List<Inventory> selectInventories = new List<Inventory> { };
            foreach (var inv in fullInventories)
            {
                if (inv.SiteId == siteId)
                {
                    inv.Site = null;
                    selectInventories.Add(inv);
                }
            }
            return selectInventories;
        }

        // Returns only the most recent inventory of the input list of inventories
        public List<Inventory> LatestInventories(List<Inventory> inputInventories)
        {
            List<Inventory> latestInventories = new List<Inventory> { };
            foreach (var inputInv in inputInventories)
            {
                DateTime inputDate = DateTime.ParseExact(inputInv.Date, "yyyy-MM-ddTHH:mm", null);
                
                // If there are already inventories in the list, iterate through them to verify which one is the latest
                // Replace older reading with newer reading
                if (latestInventories.Count > 0)
                {
                    Boolean added = false;
                    foreach (var latestInv in latestInventories.ToArray())
                    {
                        DateTime latestDate = DateTime.ParseExact(latestInv.Date, "yyyy-MM-ddTHH:mm", null);
                        if (latestInv.TankNumber == inputInv.TankNumber && DateTime.Compare(latestDate, inputDate) < 0)
                        {
                            inputInv.Product = FindProduct(inputInv.SiteId, inputInv.TankNumber);

                            latestInventories.Remove(latestInv);
                            latestInventories.Add(inputInv);
                            added = true;
                            break;
                        } else if (latestInv.TankNumber == inputInv.TankNumber)
                        {
                            inputInv.Product = FindProduct(inputInv.SiteId, inputInv.TankNumber);
                            added = true;
                            break;
                        }
                    }

                    // If no readings for given tank yet, add to list
                    if (added == false)
                    {
                        inputInv.Product = FindProduct(inputInv.SiteId, inputInv.TankNumber);
                        latestInventories.Add(inputInv);
                    }
                // No readings in list, add first reading
                } else
                {
                    inputInv.Product = FindProduct(inputInv.SiteId, inputInv.TankNumber);
                    latestInventories.Add(inputInv);
                }
            }
            return latestInventories;
        }

        // Returns list of tanks belonging to input Site ID
        public List<Tank> FetchTanks(int siteId)
        {
            List<Tank> fullTanks = _siteContext.Tanks.ToList();
            List<Tank> selectTanks = new List<Tank> { };
            foreach (var tank in fullTanks)
            {
                if (tank.SiteId == siteId)
                {
                    selectTanks.Add(tank);
                }
            }
            return selectTanks;
        }

        // Returns the full list of sites
        public List<Site> FetchSites()
        {
            return _siteContext.Sites.ToList();
        }

        private string FindProduct(int siteId, int tankNumber)
        {
            List<Tank> tanks = FetchTanks(siteId);
            foreach (var tank in tanks)
            {
                if (tank.TankNumber == tankNumber)
                {
                    return tank.TankProduct;
                }
            }
            return "";
        }
    }
}
