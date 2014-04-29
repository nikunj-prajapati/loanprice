using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
 
namespace BLL
{
  public  class USDCurveBL
    {
      public void ImportUSDCurves(List<USDCurve> lst)
      {
          try
          {
              using (LoanPriceEntities context = new LoanPriceEntities())
              {
                  foreach (var item in lst)
                  {
                      context.AddToUSDCurves(item);
                  }
                  context.SaveChanges();
              }
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
      public void UpdateCurve()
      {
          try
          {
          using (LoanPriceEntities context = new LoanPriceEntities())
          {
              var result = context.USDCurves.ToList();
              if (result != null )
              {
                  foreach (var item in result)
                  {
                      item.IsNew = false;
                  }
                  context.SaveChanges();
              }
            
          }

          }
          catch (Exception)
          {

          }

      }

      public List<USDCurve> GetUSCurve()
      {
          try
          {
              using (LoanPriceEntities contex = new LoanPriceEntities())
              {
                  return contex.USDCurves.Where(s => s.IsNew == true).ToList(); // added condition on 22-04
              }
          }
          catch (Exception)
          {
              return null;
          }

      }
    }
}
