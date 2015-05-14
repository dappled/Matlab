/*
* MATLAB Compiler: 5.1 (R2014a)
* Date: Thu May 14 12:56:06 2015
* Arguments: "-B" "macro_default" "-W" "dotnet:flexFit,FlexFit,0.0,private" "-T"
* "link:lib" "-d" "C:\Users\Dappled\OneDrive\dappled's
* sky\cs\matlab\convexMonotoneFit\flexFit\for_testing" "-v" "C:\Program
* Files\MATLAB\R2014a\toolbox\javabuilder\Examples\MagicSquareExample\MagicDemoComp\flexTi
* meFit.m" "C:\Program
* Files\MATLAB\R2014a\toolbox\javabuilder\Examples\MagicSquareExample\MagicDemoComp\flexWi
* ngFit.m" "class{FlexFit:C:\Program
* Files\MATLAB\R2014a\toolbox\javabuilder\Examples\MagicSquareExample\MagicDemoComp\flexTi
* meFit.m,C:\Program
* Files\MATLAB\R2014a\toolbox\javabuilder\Examples\MagicSquareExample\MagicDemoComp\flexWi
* ngFit.m}" "-a" "C:\Program Files\MATLAB\R2014a\toolbox\curvefit\splines\csaps.m" "-a"
* "C:\Program Files\MATLAB\R2014a\toolbox\matlab\optimfun\fminbnd.m" "-a" "C:\Program
* Files\MATLAB\R2014a\toolbox\matlab\polyfun\polyfit.m" "-a" "C:\Program
* Files\MATLAB\R2014a\toolbox\optim\optim\quadprog.m" "-a" "C:\Program
* Files\MATLAB\R2014a\toolbox\javabuilder\Examples\MagicSquareExample\MagicDemoComp\spline
* Helper.m" 
*/
using System;
using System.Reflection;
using System.IO;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

#if SHARED
[assembly: System.Reflection.AssemblyKeyFile(@"")]
#endif

namespace flexFitNative
{

  /// <summary>
  /// The FlexFit class provides a CLS compliant, Object (native) interface to the MATLAB
  /// functions contained in the files:
  /// <newpara></newpara>
  /// C:\Program
  /// Files\MATLAB\R2014a\toolbox\javabuilder\Examples\MagicSquareExample\MagicDemoComp\fl
  /// exTimeFit.m
  /// <newpara></newpara>
  /// C:\Program
  /// Files\MATLAB\R2014a\toolbox\javabuilder\Examples\MagicSquareExample\MagicDemoComp\fl
  /// exWingFit.m
  /// <newpara></newpara>
  /// deployprint.m
  /// <newpara></newpara>
  /// printdlg.m
  /// </summary>
  /// <remarks>
  /// @Version 0.0
  /// </remarks>
  public class FlexFit : IDisposable
  {
    #region Constructors

    /// <summary internal= "true">
    /// The static constructor instantiates and initializes the MATLAB Compiler Runtime
    /// instance.
    /// </summary>
    static FlexFit()
    {
      if (MWMCR.MCRAppInitialized)
      {
        try
        {
          Assembly assembly= Assembly.GetExecutingAssembly();

          string ctfFilePath= assembly.Location;

          int lastDelimiter= ctfFilePath.LastIndexOf(@"\");

          ctfFilePath= ctfFilePath.Remove(lastDelimiter, (ctfFilePath.Length - lastDelimiter));

          string ctfFileName = "flexFit.ctf";

          Stream embeddedCtfStream = null;

          String[] resourceStrings = assembly.GetManifestResourceNames();

          foreach (String name in resourceStrings)
          {
            if (name.Contains(ctfFileName))
            {
              embeddedCtfStream = assembly.GetManifestResourceStream(name);
              break;
            }
          }
          mcr= new MWMCR("",
                         ctfFilePath, embeddedCtfStream, true);
        }
        catch(Exception ex)
        {
          ex_ = new Exception("MWArray assembly failed to be initialized", ex);
        }
      }
      else
      {
        ex_ = new ApplicationException("MWArray assembly could not be initialized");
      }
    }


    /// <summary>
    /// Constructs a new instance of the FlexFit class.
    /// </summary>
    public FlexFit()
    {
      if(ex_ != null)
      {
        throw ex_;
      }
    }


    #endregion Constructors

    #region Finalize

    /// <summary internal= "true">
    /// Class destructor called by the CLR garbage collector.
    /// </summary>
    ~FlexFit()
    {
      Dispose(false);
    }


    /// <summary>
    /// Frees the native resources associated with this object
    /// </summary>
    public void Dispose()
    {
      Dispose(true);

      GC.SuppressFinalize(this);
    }


    /// <summary internal= "true">
    /// Internal dispose function
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
      if (!disposed)
      {
        disposed= true;

        if (disposing)
        {
          // Free managed resources;
        }

        // Free native resources
      }
    }


    #endregion Finalize

    #region Methods

    /// <summary>
    /// Provides a single output, 0-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit()
    {
      return mcr.EvaluateFunction("flexTimeFit", new Object[]{});
    }


    /// <summary>
    /// Provides a single output, 1-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin);
    }


    /// <summary>
    /// Provides a single output, 2-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin);
    }


    /// <summary>
    /// Provides a single output, 3-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w);
    }


    /// <summary>
    /// Provides a single output, 4-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint);
    }


    /// <summary>
    /// Provides a single output, 5-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity);
    }


    /// <summary>
    /// Provides a single output, 6-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb);
    }


    /// <summary>
    /// Provides a single output, 7-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub);
    }


    /// <summary>
    /// Provides a single output, 8-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object invalidx)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx);
    }


    /// <summary>
    /// Provides a single output, 9-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper);
    }


    /// <summary>
    /// Provides a single output, 10-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower);
    }


    /// <summary>
    /// Provides a single output, 11-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff);
    }


    /// <summary>
    /// Provides a single output, 12-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff, Object boundaryx)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx);
    }


    /// <summary>
    /// Provides a single output, 13-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff, Object boundaryx, Object boundarydx)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx);
    }


    /// <summary>
    /// Provides a single output, 14-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff, Object boundaryx, Object boundarydx, Object 
                        boundarydxx)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx);
    }


    /// <summary>
    /// Provides a single output, 15-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright);
    }


    /// <summary>
    /// Provides a single output, 16-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object xexl)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl);
    }


    /// <summary>
    /// Provides a single output, 17-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object xexl, Object yexl)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl);
    }


    /// <summary>
    /// Provides a single output, 18-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object xexl, Object yexl, Object 
                        xendl)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl);
    }


    /// <summary>
    /// Provides a single output, 19-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object xexl, Object yexl, Object 
                        xendl, Object lbendl)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl);
    }


    /// <summary>
    /// Provides a single output, 20-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object xexl, Object yexl, Object 
                        xendl, Object lbendl, Object ubendl)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl);
    }


    /// <summary>
    /// Provides a single output, 21-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object xexl, Object yexl, Object 
                        xendl, Object lbendl, Object ubendl, Object xexr)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr);
    }


    /// <summary>
    /// Provides a single output, 22-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object xexl, Object yexl, Object 
                        xendl, Object lbendl, Object ubendl, Object xexr, Object yexr)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr);
    }


    /// <summary>
    /// Provides a single output, 23-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <param name="xendr">Input argument #23</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object xexl, Object yexl, Object 
                        xendl, Object lbendl, Object ubendl, Object xexr, Object yexr, 
                        Object xendr)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr, xendr);
    }


    /// <summary>
    /// Provides a single output, 24-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <param name="xendr">Input argument #23</param>
    /// <param name="lbendr">Input argument #24</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object xexl, Object yexl, Object 
                        xendl, Object lbendl, Object ubendl, Object xexr, Object yexr, 
                        Object xendr, Object lbendr)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr, xendr, lbendr);
    }


    /// <summary>
    /// Provides a single output, 25-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <param name="xendr">Input argument #23</param>
    /// <param name="lbendr">Input argument #24</param>
    /// <param name="ubendr">Input argument #25</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object xexl, Object yexl, Object 
                        xendl, Object lbendl, Object ubendl, Object xexr, Object yexr, 
                        Object xendr, Object lbendr, Object ubendr)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr, xendr, lbendr, ubendr);
    }


    /// <summary>
    /// Provides a single output, 26-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <param name="xendr">Input argument #23</param>
    /// <param name="lbendr">Input argument #24</param>
    /// <param name="ubendr">Input argument #25</param>
    /// <param name="leftincrease">Input argument #26</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object xexl, Object yexl, Object 
                        xendl, Object lbendl, Object ubendl, Object xexr, Object yexr, 
                        Object xendr, Object lbendr, Object ubendr, Object leftincrease)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr, xendr, lbendr, ubendr, leftincrease);
    }


    /// <summary>
    /// Provides a single output, 27-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <param name="xendr">Input argument #23</param>
    /// <param name="lbendr">Input argument #24</param>
    /// <param name="ubendr">Input argument #25</param>
    /// <param name="leftincrease">Input argument #26</param>
    /// <param name="rightincrease">Input argument #27</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object xexl, Object yexl, Object 
                        xendl, Object lbendl, Object ubendl, Object xexr, Object yexr, 
                        Object xendr, Object lbendr, Object ubendr, Object leftincrease, 
                        Object rightincrease)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr, xendr, lbendr, ubendr, leftincrease, rightincrease);
    }


    /// <summary>
    /// Provides a single output, 28-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <param name="xendr">Input argument #23</param>
    /// <param name="lbendr">Input argument #24</param>
    /// <param name="ubendr">Input argument #25</param>
    /// <param name="leftincrease">Input argument #26</param>
    /// <param name="rightincrease">Input argument #27</param>
    /// <param name="smooth">Input argument #28</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object xexl, Object yexl, Object 
                        xendl, Object lbendl, Object ubendl, Object xexr, Object yexr, 
                        Object xendr, Object lbendr, Object ubendr, Object leftincrease, 
                        Object rightincrease, Object smooth)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr, xendr, lbendr, ubendr, leftincrease, rightincrease, smooth);
    }


    /// <summary>
    /// Provides a single output, 29-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <param name="xendr">Input argument #23</param>
    /// <param name="lbendr">Input argument #24</param>
    /// <param name="ubendr">Input argument #25</param>
    /// <param name="leftincrease">Input argument #26</param>
    /// <param name="rightincrease">Input argument #27</param>
    /// <param name="smooth">Input argument #28</param>
    /// <param name="tight">Input argument #29</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object xexl, Object yexl, Object 
                        xendl, Object lbendl, Object ubendl, Object xexr, Object yexr, 
                        Object xendr, Object lbendr, Object ubendr, Object leftincrease, 
                        Object rightincrease, Object smooth, Object tight)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr, xendr, lbendr, ubendr, leftincrease, rightincrease, smooth, tight);
    }


    /// <summary>
    /// Provides a single output, 30-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <param name="xendr">Input argument #23</param>
    /// <param name="lbendr">Input argument #24</param>
    /// <param name="ubendr">Input argument #25</param>
    /// <param name="leftincrease">Input argument #26</param>
    /// <param name="rightincrease">Input argument #27</param>
    /// <param name="smooth">Input argument #28</param>
    /// <param name="tight">Input argument #29</param>
    /// <param name="tightlb">Input argument #30</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object xexl, Object yexl, Object 
                        xendl, Object lbendl, Object ubendl, Object xexr, Object yexr, 
                        Object xendr, Object lbendr, Object ubendr, Object leftincrease, 
                        Object rightincrease, Object smooth, Object tight, Object tightlb)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr, xendr, lbendr, ubendr, leftincrease, rightincrease, smooth, tight, tightlb);
    }


    /// <summary>
    /// Provides a single output, 31-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <param name="xendr">Input argument #23</param>
    /// <param name="lbendr">Input argument #24</param>
    /// <param name="ubendr">Input argument #25</param>
    /// <param name="leftincrease">Input argument #26</param>
    /// <param name="rightincrease">Input argument #27</param>
    /// <param name="smooth">Input argument #28</param>
    /// <param name="tight">Input argument #29</param>
    /// <param name="tightlb">Input argument #30</param>
    /// <param name="tightub">Input argument #31</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object xexl, Object yexl, Object 
                        xendl, Object lbendl, Object ubendl, Object xexr, Object yexr, 
                        Object xendr, Object lbendr, Object ubendr, Object leftincrease, 
                        Object rightincrease, Object smooth, Object tight, Object 
                        tightlb, Object tightub)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr, xendr, lbendr, ubendr, leftincrease, rightincrease, smooth, tight, tightlb, tightub);
    }


    /// <summary>
    /// Provides a single output, 32-input Objectinterface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <param name="xendr">Input argument #23</param>
    /// <param name="lbendr">Input argument #24</param>
    /// <param name="ubendr">Input argument #25</param>
    /// <param name="leftincrease">Input argument #26</param>
    /// <param name="rightincrease">Input argument #27</param>
    /// <param name="smooth">Input argument #28</param>
    /// <param name="tight">Input argument #29</param>
    /// <param name="tightlb">Input argument #30</param>
    /// <param name="tightub">Input argument #31</param>
    /// <param name="minxrange">Input argument #32</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexTimeFit(Object xin, Object yin, Object w, Object stationarypoint, 
                        Object tailConcavity, Object xinlb, Object xinub, Object 
                        invalidx, Object invalidupper, Object invalidlower, Object 
                        smoothCoeff, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object xexl, Object yexl, Object 
                        xendl, Object lbendl, Object ubendl, Object xexr, Object yexr, 
                        Object xendr, Object lbendr, Object ubendr, Object leftincrease, 
                        Object rightincrease, Object smooth, Object tight, Object 
                        tightlb, Object tightub, Object minxrange)
    {
      return mcr.EvaluateFunction("flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr, xendr, lbendr, ubendr, leftincrease, rightincrease, smooth, tight, tightlb, tightub, minxrange);
    }


    /// <summary>
    /// Provides the standard 0-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", new Object[]{});
    }


    /// <summary>
    /// Provides the standard 1-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin);
    }


    /// <summary>
    /// Provides the standard 2-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin);
    }


    /// <summary>
    /// Provides the standard 3-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w);
    }


    /// <summary>
    /// Provides the standard 4-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint);
    }


    /// <summary>
    /// Provides the standard 5-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity);
    }


    /// <summary>
    /// Provides the standard 6-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb);
    }


    /// <summary>
    /// Provides the standard 7-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub);
    }


    /// <summary>
    /// Provides the standard 8-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx);
    }


    /// <summary>
    /// Provides the standard 9-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper);
    }


    /// <summary>
    /// Provides the standard 10-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower);
    }


    /// <summary>
    /// Provides the standard 11-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff);
    }


    /// <summary>
    /// Provides the standard 12-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff, Object boundaryx)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx);
    }


    /// <summary>
    /// Provides the standard 13-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff, Object boundaryx, Object 
                          boundarydx)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx);
    }


    /// <summary>
    /// Provides the standard 14-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff, Object boundaryx, Object 
                          boundarydx, Object boundarydxx)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx);
    }


    /// <summary>
    /// Provides the standard 15-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright);
    }


    /// <summary>
    /// Provides the standard 16-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object xexl)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl);
    }


    /// <summary>
    /// Provides the standard 17-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object xexl, 
                          Object yexl)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl);
    }


    /// <summary>
    /// Provides the standard 18-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object xexl, 
                          Object yexl, Object xendl)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl);
    }


    /// <summary>
    /// Provides the standard 19-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object xexl, 
                          Object yexl, Object xendl, Object lbendl)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl);
    }


    /// <summary>
    /// Provides the standard 20-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object xexl, 
                          Object yexl, Object xendl, Object lbendl, Object ubendl)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl);
    }


    /// <summary>
    /// Provides the standard 21-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object xexl, 
                          Object yexl, Object xendl, Object lbendl, Object ubendl, Object 
                          xexr)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr);
    }


    /// <summary>
    /// Provides the standard 22-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object xexl, 
                          Object yexl, Object xendl, Object lbendl, Object ubendl, Object 
                          xexr, Object yexr)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr);
    }


    /// <summary>
    /// Provides the standard 23-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <param name="xendr">Input argument #23</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object xexl, 
                          Object yexl, Object xendl, Object lbendl, Object ubendl, Object 
                          xexr, Object yexr, Object xendr)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr, xendr);
    }


    /// <summary>
    /// Provides the standard 24-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <param name="xendr">Input argument #23</param>
    /// <param name="lbendr">Input argument #24</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object xexl, 
                          Object yexl, Object xendl, Object lbendl, Object ubendl, Object 
                          xexr, Object yexr, Object xendr, Object lbendr)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr, xendr, lbendr);
    }


    /// <summary>
    /// Provides the standard 25-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <param name="xendr">Input argument #23</param>
    /// <param name="lbendr">Input argument #24</param>
    /// <param name="ubendr">Input argument #25</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object xexl, 
                          Object yexl, Object xendl, Object lbendl, Object ubendl, Object 
                          xexr, Object yexr, Object xendr, Object lbendr, Object ubendr)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr, xendr, lbendr, ubendr);
    }


    /// <summary>
    /// Provides the standard 26-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <param name="xendr">Input argument #23</param>
    /// <param name="lbendr">Input argument #24</param>
    /// <param name="ubendr">Input argument #25</param>
    /// <param name="leftincrease">Input argument #26</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object xexl, 
                          Object yexl, Object xendl, Object lbendl, Object ubendl, Object 
                          xexr, Object yexr, Object xendr, Object lbendr, Object ubendr, 
                          Object leftincrease)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr, xendr, lbendr, ubendr, leftincrease);
    }


    /// <summary>
    /// Provides the standard 27-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <param name="xendr">Input argument #23</param>
    /// <param name="lbendr">Input argument #24</param>
    /// <param name="ubendr">Input argument #25</param>
    /// <param name="leftincrease">Input argument #26</param>
    /// <param name="rightincrease">Input argument #27</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object xexl, 
                          Object yexl, Object xendl, Object lbendl, Object ubendl, Object 
                          xexr, Object yexr, Object xendr, Object lbendr, Object ubendr, 
                          Object leftincrease, Object rightincrease)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr, xendr, lbendr, ubendr, leftincrease, rightincrease);
    }


    /// <summary>
    /// Provides the standard 28-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <param name="xendr">Input argument #23</param>
    /// <param name="lbendr">Input argument #24</param>
    /// <param name="ubendr">Input argument #25</param>
    /// <param name="leftincrease">Input argument #26</param>
    /// <param name="rightincrease">Input argument #27</param>
    /// <param name="smooth">Input argument #28</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object xexl, 
                          Object yexl, Object xendl, Object lbendl, Object ubendl, Object 
                          xexr, Object yexr, Object xendr, Object lbendr, Object ubendr, 
                          Object leftincrease, Object rightincrease, Object smooth)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr, xendr, lbendr, ubendr, leftincrease, rightincrease, smooth);
    }


    /// <summary>
    /// Provides the standard 29-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <param name="xendr">Input argument #23</param>
    /// <param name="lbendr">Input argument #24</param>
    /// <param name="ubendr">Input argument #25</param>
    /// <param name="leftincrease">Input argument #26</param>
    /// <param name="rightincrease">Input argument #27</param>
    /// <param name="smooth">Input argument #28</param>
    /// <param name="tight">Input argument #29</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object xexl, 
                          Object yexl, Object xendl, Object lbendl, Object ubendl, Object 
                          xexr, Object yexr, Object xendr, Object lbendr, Object ubendr, 
                          Object leftincrease, Object rightincrease, Object smooth, 
                          Object tight)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr, xendr, lbendr, ubendr, leftincrease, rightincrease, smooth, tight);
    }


    /// <summary>
    /// Provides the standard 30-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <param name="xendr">Input argument #23</param>
    /// <param name="lbendr">Input argument #24</param>
    /// <param name="ubendr">Input argument #25</param>
    /// <param name="leftincrease">Input argument #26</param>
    /// <param name="rightincrease">Input argument #27</param>
    /// <param name="smooth">Input argument #28</param>
    /// <param name="tight">Input argument #29</param>
    /// <param name="tightlb">Input argument #30</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object xexl, 
                          Object yexl, Object xendl, Object lbendl, Object ubendl, Object 
                          xexr, Object yexr, Object xendr, Object lbendr, Object ubendr, 
                          Object leftincrease, Object rightincrease, Object smooth, 
                          Object tight, Object tightlb)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr, xendr, lbendr, ubendr, leftincrease, rightincrease, smooth, tight, tightlb);
    }


    /// <summary>
    /// Provides the standard 31-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <param name="xendr">Input argument #23</param>
    /// <param name="lbendr">Input argument #24</param>
    /// <param name="ubendr">Input argument #25</param>
    /// <param name="leftincrease">Input argument #26</param>
    /// <param name="rightincrease">Input argument #27</param>
    /// <param name="smooth">Input argument #28</param>
    /// <param name="tight">Input argument #29</param>
    /// <param name="tightlb">Input argument #30</param>
    /// <param name="tightub">Input argument #31</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object xexl, 
                          Object yexl, Object xendl, Object lbendl, Object ubendl, Object 
                          xexr, Object yexr, Object xendr, Object lbendr, Object ubendr, 
                          Object leftincrease, Object rightincrease, Object smooth, 
                          Object tight, Object tightlb, Object tightub)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr, xendr, lbendr, ubendr, leftincrease, rightincrease, smooth, tight, tightlb, tightub);
    }


    /// <summary>
    /// Provides the standard 32-input Object interface to the flexTimeFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="xin">Input argument #1</param>
    /// <param name="yin">Input argument #2</param>
    /// <param name="w">Input argument #3</param>
    /// <param name="stationarypoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="xinlb">Input argument #6</param>
    /// <param name="xinub">Input argument #7</param>
    /// <param name="invalidx">Input argument #8</param>
    /// <param name="invalidupper">Input argument #9</param>
    /// <param name="invalidlower">Input argument #10</param>
    /// <param name="smoothCoeff">Input argument #11</param>
    /// <param name="boundaryx">Input argument #12</param>
    /// <param name="boundarydx">Input argument #13</param>
    /// <param name="boundarydxx">Input argument #14</param>
    /// <param name="leftright">Input argument #15</param>
    /// <param name="xexl">Input argument #16</param>
    /// <param name="yexl">Input argument #17</param>
    /// <param name="xendl">Input argument #18</param>
    /// <param name="lbendl">Input argument #19</param>
    /// <param name="ubendl">Input argument #20</param>
    /// <param name="xexr">Input argument #21</param>
    /// <param name="yexr">Input argument #22</param>
    /// <param name="xendr">Input argument #23</param>
    /// <param name="lbendr">Input argument #24</param>
    /// <param name="ubendr">Input argument #25</param>
    /// <param name="leftincrease">Input argument #26</param>
    /// <param name="rightincrease">Input argument #27</param>
    /// <param name="smooth">Input argument #28</param>
    /// <param name="tight">Input argument #29</param>
    /// <param name="tightlb">Input argument #30</param>
    /// <param name="tightub">Input argument #31</param>
    /// <param name="minxrange">Input argument #32</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexTimeFit(int numArgsOut, Object xin, Object yin, Object w, Object 
                          stationarypoint, Object tailConcavity, Object xinlb, Object 
                          xinub, Object invalidx, Object invalidupper, Object 
                          invalidlower, Object smoothCoeff, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object xexl, 
                          Object yexl, Object xendl, Object lbendl, Object ubendl, Object 
                          xexr, Object yexr, Object xendr, Object lbendr, Object ubendr, 
                          Object leftincrease, Object rightincrease, Object smooth, 
                          Object tight, Object tightlb, Object tightub, Object minxrange)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexTimeFit", xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl, ubendl, xexr, yexr, xendr, lbendr, ubendr, leftincrease, rightincrease, smooth, tight, tightlb, tightub, minxrange);
    }


    /// <summary>
    /// Provides an interface for the flexTimeFit function in which the input and output
    /// arguments are specified as an array of Objects.
    /// </summary>
    /// <remarks>
    /// This method will allocate and return by reference the output argument
    /// array.<newpara></newpara>
    /// M-Documentation:
    /// input
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
    /// leftright = nan;
    /// smoothCoeff = nan;
    /// stationarypoint = nan;
    /// i = find(M(:,1)==1);
    /// boundaryx = [nan; nan];
    /// boundarydx = [nan; nan];
    /// boundarydxx = [nan; nan];
    /// if isnan(leftright)
    /// intropart = i(1) : i(end);
    /// iEndl = 1 : i(1) - 1;
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// elseif leftright == -1
    /// intropart = i(1) : size(M,1);
    /// iEndl = 1 : (i(1) - 1);
    /// iEndlvalid = find(~isnan(M(iEndl, 4)));
    /// ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
    /// iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
    /// else
    /// intropart = 1: i(end);
    /// iEndr = (i(end) + 1) : size(M,1);
    /// iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
    /// ivalid = find(~isnan(M(intropart, 4)));
    /// iinvalid = find(isnan(M(intropart, 4)));
    /// end
    /// xin = M(ivalid,2)';
    /// invalidx = M(iinvalid,2)';
    /// yin = M(ivalid,4)';
    /// w = M(ivalid,5)';
    /// xinub = M(ivalid,7)';  inf(1,n);
    /// xinub(xinub == 1) = inf;
    /// invalidupper = M(iinvalid, 7)';
    /// invalidupper(invalidupper == 1) = inf;
    /// invalidlower = M(iinvalid, 6);
    /// xinlb = M(ivalid,6)';  zeros(1,n);
    /// if isnan(leftright)
    /// xexl = M(iEndlvalid, 2)';
    /// yexl = M(iEndlvalid, 4)';
    /// xendl = M(iEndl,2)';
    /// xexr = M(iEndrvalid, 2)';
    /// yexr = M(iEndrvalid, 4)';
    /// xendr = M(iEndr,2)';
    /// ubendl = M(iEndl, 7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = M(iEndr, 7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = M(iEndl, 6)';
    /// lbendr = M(iEndr, 6)';
    /// elseif leftright == -1
    /// xexl = M(iEndlvalid,2)';
    /// xexr = [];
    /// yexl = M(iEndlvalid,4)';
    /// yexr = [];
    /// xendl = M(iEndl,2)';
    /// xendr = [];
    /// ubendl = M(iEndl,7)';
    /// ubendl(ubendl == 1) = inf;
    /// ubendr = [];
    /// lbendl = M(iEndl,6)';
    /// lbendr = [];
    /// else
    /// xexl = [];
    /// xexr = M(iEndrvalid,2)';
    /// yexl = [];
    /// yexr = M(iEndrvalid,4)';
    /// xendl = [];
    /// xendr = M(iEndr,2)';
    /// ubendl = [];
    /// ubendr = M(iEndr,7)';
    /// ubendr(ubendr == 1) = inf;
    /// lbendl = [];
    /// lbendr = M(iEndr,6)';
    /// end
    /// leftincrease = -inf;
    /// rightincrease = inf;
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return</param>
    /// <param name= "argsOut">Array of Object output arguments</param>
    /// <param name= "argsIn">Array of Object input arguments</param>
    /// <param name= "varArgsIn">Array of Object representing variable input
    /// arguments</param>
    ///
    [MATLABSignature("flexTimeFit", 32, 10, 0)]
    protected void flexTimeFit(int numArgsOut, ref Object[] argsOut, Object[] argsIn, params Object[] varArgsIn)
    {
        mcr.EvaluateFunctionForTypeSafeCall("flexTimeFit", numArgsOut, ref argsOut, argsIn, varArgsIn);
    }
    /// <summary>
    /// Provides a single output, 0-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit()
    {
      return mcr.EvaluateFunction("flexWingFit", new Object[]{});
    }


    /// <summary>
    /// Provides a single output, 1-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1);
    }


    /// <summary>
    /// Provides a single output, 2-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y);
    }


    /// <summary>
    /// Provides a single output, 3-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight);
    }


    /// <summary>
    /// Provides a single output, 4-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint);
    }


    /// <summary>
    /// Provides a single output, 5-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity);
    }


    /// <summary>
    /// Provides a single output, 6-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff);
    }


    /// <summary>
    /// Provides a single output, 7-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1);
    }


    /// <summary>
    /// Provides a single output, 8-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx);
    }


    /// <summary>
    /// Provides a single output, 9-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx);
    }


    /// <summary>
    /// Provides a single output, 10-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx, Object 
                        boundarydxx)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx);
    }


    /// <summary>
    /// Provides a single output, 11-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright);
    }


    /// <summary>
    /// Provides a single output, 12-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object upperLimitG)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG);
    }


    /// <summary>
    /// Provides a single output, 13-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object upperLimitG, Object 
                        lowerLimitG)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG);
    }


    /// <summary>
    /// Provides a single output, 14-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object upperLimitG, Object 
                        lowerLimitG, Object xEndl)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl);
    }


    /// <summary>
    /// Provides a single output, 15-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object upperLimitG, Object 
                        lowerLimitG, Object xEndl, Object aMaxl)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl);
    }


    /// <summary>
    /// Provides a single output, 16-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object upperLimitG, Object 
                        lowerLimitG, Object xEndl, Object aMaxl, Object aMinl)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl);
    }


    /// <summary>
    /// Provides a single output, 17-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object upperLimitG, Object 
                        lowerLimitG, Object xEndl, Object aMaxl, Object aMinl, Object 
                        xEndr)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr);
    }


    /// <summary>
    /// Provides a single output, 18-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object upperLimitG, Object 
                        lowerLimitG, Object xEndl, Object aMaxl, Object aMinl, Object 
                        xEndr, Object aMaxr)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr);
    }


    /// <summary>
    /// Provides a single output, 19-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object upperLimitG, Object 
                        lowerLimitG, Object xEndl, Object aMaxl, Object aMinl, Object 
                        xEndr, Object aMaxr, Object aMinr)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr);
    }


    /// <summary>
    /// Provides a single output, 20-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object upperLimitG, Object 
                        lowerLimitG, Object xEndl, Object aMaxl, Object aMinl, Object 
                        xEndr, Object aMaxr, Object aMinr, Object invalidx)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx);
    }


    /// <summary>
    /// Provides a single output, 21-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <param name="invalidupper">Input argument #21</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object upperLimitG, Object 
                        lowerLimitG, Object xEndl, Object aMaxl, Object aMinl, Object 
                        xEndr, Object aMaxr, Object aMinr, Object invalidx, Object 
                        invalidupper)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx, invalidupper);
    }


    /// <summary>
    /// Provides a single output, 22-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <param name="invalidupper">Input argument #21</param>
    /// <param name="invalidlower">Input argument #22</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object upperLimitG, Object 
                        lowerLimitG, Object xEndl, Object aMaxl, Object aMinl, Object 
                        xEndr, Object aMaxr, Object aMinr, Object invalidx, Object 
                        invalidupper, Object invalidlower)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx, invalidupper, invalidlower);
    }


    /// <summary>
    /// Provides a single output, 23-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <param name="invalidupper">Input argument #21</param>
    /// <param name="invalidlower">Input argument #22</param>
    /// <param name="leftincrease">Input argument #23</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object upperLimitG, Object 
                        lowerLimitG, Object xEndl, Object aMaxl, Object aMinl, Object 
                        xEndr, Object aMaxr, Object aMinr, Object invalidx, Object 
                        invalidupper, Object invalidlower, Object leftincrease)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx, invalidupper, invalidlower, leftincrease);
    }


    /// <summary>
    /// Provides a single output, 24-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <param name="invalidupper">Input argument #21</param>
    /// <param name="invalidlower">Input argument #22</param>
    /// <param name="leftincrease">Input argument #23</param>
    /// <param name="rightincrease">Input argument #24</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object upperLimitG, Object 
                        lowerLimitG, Object xEndl, Object aMaxl, Object aMinl, Object 
                        xEndr, Object aMaxr, Object aMinr, Object invalidx, Object 
                        invalidupper, Object invalidlower, Object leftincrease, Object 
                        rightincrease)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx, invalidupper, invalidlower, leftincrease, rightincrease);
    }


    /// <summary>
    /// Provides a single output, 25-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <param name="invalidupper">Input argument #21</param>
    /// <param name="invalidlower">Input argument #22</param>
    /// <param name="leftincrease">Input argument #23</param>
    /// <param name="rightincrease">Input argument #24</param>
    /// <param name="smooth">Input argument #25</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object upperLimitG, Object 
                        lowerLimitG, Object xEndl, Object aMaxl, Object aMinl, Object 
                        xEndr, Object aMaxr, Object aMinr, Object invalidx, Object 
                        invalidupper, Object invalidlower, Object leftincrease, Object 
                        rightincrease, Object smooth)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx, invalidupper, invalidlower, leftincrease, rightincrease, smooth);
    }


    /// <summary>
    /// Provides a single output, 26-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <param name="invalidupper">Input argument #21</param>
    /// <param name="invalidlower">Input argument #22</param>
    /// <param name="leftincrease">Input argument #23</param>
    /// <param name="rightincrease">Input argument #24</param>
    /// <param name="smooth">Input argument #25</param>
    /// <param name="tight">Input argument #26</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object upperLimitG, Object 
                        lowerLimitG, Object xEndl, Object aMaxl, Object aMinl, Object 
                        xEndr, Object aMaxr, Object aMinr, Object invalidx, Object 
                        invalidupper, Object invalidlower, Object leftincrease, Object 
                        rightincrease, Object smooth, Object tight)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx, invalidupper, invalidlower, leftincrease, rightincrease, smooth, tight);
    }


    /// <summary>
    /// Provides a single output, 27-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <param name="invalidupper">Input argument #21</param>
    /// <param name="invalidlower">Input argument #22</param>
    /// <param name="leftincrease">Input argument #23</param>
    /// <param name="rightincrease">Input argument #24</param>
    /// <param name="smooth">Input argument #25</param>
    /// <param name="tight">Input argument #26</param>
    /// <param name="tightlb">Input argument #27</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object upperLimitG, Object 
                        lowerLimitG, Object xEndl, Object aMaxl, Object aMinl, Object 
                        xEndr, Object aMaxr, Object aMinr, Object invalidx, Object 
                        invalidupper, Object invalidlower, Object leftincrease, Object 
                        rightincrease, Object smooth, Object tight, Object tightlb)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx, invalidupper, invalidlower, leftincrease, rightincrease, smooth, tight, tightlb);
    }


    /// <summary>
    /// Provides a single output, 28-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <param name="invalidupper">Input argument #21</param>
    /// <param name="invalidlower">Input argument #22</param>
    /// <param name="leftincrease">Input argument #23</param>
    /// <param name="rightincrease">Input argument #24</param>
    /// <param name="smooth">Input argument #25</param>
    /// <param name="tight">Input argument #26</param>
    /// <param name="tightlb">Input argument #27</param>
    /// <param name="tightub">Input argument #28</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object upperLimitG, Object 
                        lowerLimitG, Object xEndl, Object aMaxl, Object aMinl, Object 
                        xEndr, Object aMaxr, Object aMinr, Object invalidx, Object 
                        invalidupper, Object invalidlower, Object leftincrease, Object 
                        rightincrease, Object smooth, Object tight, Object tightlb, 
                        Object tightub)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx, invalidupper, invalidlower, leftincrease, rightincrease, smooth, tight, tightlb, tightub);
    }


    /// <summary>
    /// Provides a single output, 29-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <param name="invalidupper">Input argument #21</param>
    /// <param name="invalidlower">Input argument #22</param>
    /// <param name="leftincrease">Input argument #23</param>
    /// <param name="rightincrease">Input argument #24</param>
    /// <param name="smooth">Input argument #25</param>
    /// <param name="tight">Input argument #26</param>
    /// <param name="tightlb">Input argument #27</param>
    /// <param name="tightub">Input argument #28</param>
    /// <param name="minxrange">Input argument #29</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object upperLimitG, Object 
                        lowerLimitG, Object xEndl, Object aMaxl, Object aMinl, Object 
                        xEndr, Object aMaxr, Object aMinr, Object invalidx, Object 
                        invalidupper, Object invalidlower, Object leftincrease, Object 
                        rightincrease, Object smooth, Object tight, Object tightlb, 
                        Object tightub, Object minxrange)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx, invalidupper, invalidlower, leftincrease, rightincrease, smooth, tight, tightlb, tightub, minxrange);
    }


    /// <summary>
    /// Provides a single output, 30-input Objectinterface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <param name="invalidupper">Input argument #21</param>
    /// <param name="invalidlower">Input argument #22</param>
    /// <param name="leftincrease">Input argument #23</param>
    /// <param name="rightincrease">Input argument #24</param>
    /// <param name="smooth">Input argument #25</param>
    /// <param name="tight">Input argument #26</param>
    /// <param name="tightlb">Input argument #27</param>
    /// <param name="tightub">Input argument #28</param>
    /// <param name="minxrange">Input argument #29</param>
    /// <param name="concave">Input argument #30</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object flexWingFit(Object x_in1, Object y, Object weight, Object 
                        stationaryPoint, Object tailConcavity, Object smoothCoeff, Object 
                        turningPoint_in1, Object boundaryx, Object boundarydx, Object 
                        boundarydxx, Object leftright, Object upperLimitG, Object 
                        lowerLimitG, Object xEndl, Object aMaxl, Object aMinl, Object 
                        xEndr, Object aMaxr, Object aMinr, Object invalidx, Object 
                        invalidupper, Object invalidlower, Object leftincrease, Object 
                        rightincrease, Object smooth, Object tight, Object tightlb, 
                        Object tightub, Object minxrange, Object concave)
    {
      return mcr.EvaluateFunction("flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx, invalidupper, invalidlower, leftincrease, rightincrease, smooth, tight, tightlb, tightub, minxrange, concave);
    }


    /// <summary>
    /// Provides the standard 0-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", new Object[]{});
    }


    /// <summary>
    /// Provides the standard 1-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1);
    }


    /// <summary>
    /// Provides the standard 2-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y);
    }


    /// <summary>
    /// Provides the standard 3-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight);
    }


    /// <summary>
    /// Provides the standard 4-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint);
    }


    /// <summary>
    /// Provides the standard 5-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity);
    }


    /// <summary>
    /// Provides the standard 6-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff);
    }


    /// <summary>
    /// Provides the standard 7-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1);
    }


    /// <summary>
    /// Provides the standard 8-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx);
    }


    /// <summary>
    /// Provides the standard 9-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx);
    }


    /// <summary>
    /// Provides the standard 10-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx, Object boundarydxx)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx);
    }


    /// <summary>
    /// Provides the standard 11-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright);
    }


    /// <summary>
    /// Provides the standard 12-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object 
                          upperLimitG)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG);
    }


    /// <summary>
    /// Provides the standard 13-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object 
                          upperLimitG, Object lowerLimitG)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG);
    }


    /// <summary>
    /// Provides the standard 14-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object 
                          upperLimitG, Object lowerLimitG, Object xEndl)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl);
    }


    /// <summary>
    /// Provides the standard 15-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object 
                          upperLimitG, Object lowerLimitG, Object xEndl, Object aMaxl)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl);
    }


    /// <summary>
    /// Provides the standard 16-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object 
                          upperLimitG, Object lowerLimitG, Object xEndl, Object aMaxl, 
                          Object aMinl)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl);
    }


    /// <summary>
    /// Provides the standard 17-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object 
                          upperLimitG, Object lowerLimitG, Object xEndl, Object aMaxl, 
                          Object aMinl, Object xEndr)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr);
    }


    /// <summary>
    /// Provides the standard 18-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object 
                          upperLimitG, Object lowerLimitG, Object xEndl, Object aMaxl, 
                          Object aMinl, Object xEndr, Object aMaxr)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr);
    }


    /// <summary>
    /// Provides the standard 19-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object 
                          upperLimitG, Object lowerLimitG, Object xEndl, Object aMaxl, 
                          Object aMinl, Object xEndr, Object aMaxr, Object aMinr)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr);
    }


    /// <summary>
    /// Provides the standard 20-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object 
                          upperLimitG, Object lowerLimitG, Object xEndl, Object aMaxl, 
                          Object aMinl, Object xEndr, Object aMaxr, Object aMinr, Object 
                          invalidx)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx);
    }


    /// <summary>
    /// Provides the standard 21-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <param name="invalidupper">Input argument #21</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object 
                          upperLimitG, Object lowerLimitG, Object xEndl, Object aMaxl, 
                          Object aMinl, Object xEndr, Object aMaxr, Object aMinr, Object 
                          invalidx, Object invalidupper)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx, invalidupper);
    }


    /// <summary>
    /// Provides the standard 22-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <param name="invalidupper">Input argument #21</param>
    /// <param name="invalidlower">Input argument #22</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object 
                          upperLimitG, Object lowerLimitG, Object xEndl, Object aMaxl, 
                          Object aMinl, Object xEndr, Object aMaxr, Object aMinr, Object 
                          invalidx, Object invalidupper, Object invalidlower)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx, invalidupper, invalidlower);
    }


    /// <summary>
    /// Provides the standard 23-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <param name="invalidupper">Input argument #21</param>
    /// <param name="invalidlower">Input argument #22</param>
    /// <param name="leftincrease">Input argument #23</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object 
                          upperLimitG, Object lowerLimitG, Object xEndl, Object aMaxl, 
                          Object aMinl, Object xEndr, Object aMaxr, Object aMinr, Object 
                          invalidx, Object invalidupper, Object invalidlower, Object 
                          leftincrease)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx, invalidupper, invalidlower, leftincrease);
    }


    /// <summary>
    /// Provides the standard 24-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <param name="invalidupper">Input argument #21</param>
    /// <param name="invalidlower">Input argument #22</param>
    /// <param name="leftincrease">Input argument #23</param>
    /// <param name="rightincrease">Input argument #24</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object 
                          upperLimitG, Object lowerLimitG, Object xEndl, Object aMaxl, 
                          Object aMinl, Object xEndr, Object aMaxr, Object aMinr, Object 
                          invalidx, Object invalidupper, Object invalidlower, Object 
                          leftincrease, Object rightincrease)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx, invalidupper, invalidlower, leftincrease, rightincrease);
    }


    /// <summary>
    /// Provides the standard 25-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <param name="invalidupper">Input argument #21</param>
    /// <param name="invalidlower">Input argument #22</param>
    /// <param name="leftincrease">Input argument #23</param>
    /// <param name="rightincrease">Input argument #24</param>
    /// <param name="smooth">Input argument #25</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object 
                          upperLimitG, Object lowerLimitG, Object xEndl, Object aMaxl, 
                          Object aMinl, Object xEndr, Object aMaxr, Object aMinr, Object 
                          invalidx, Object invalidupper, Object invalidlower, Object 
                          leftincrease, Object rightincrease, Object smooth)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx, invalidupper, invalidlower, leftincrease, rightincrease, smooth);
    }


    /// <summary>
    /// Provides the standard 26-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <param name="invalidupper">Input argument #21</param>
    /// <param name="invalidlower">Input argument #22</param>
    /// <param name="leftincrease">Input argument #23</param>
    /// <param name="rightincrease">Input argument #24</param>
    /// <param name="smooth">Input argument #25</param>
    /// <param name="tight">Input argument #26</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object 
                          upperLimitG, Object lowerLimitG, Object xEndl, Object aMaxl, 
                          Object aMinl, Object xEndr, Object aMaxr, Object aMinr, Object 
                          invalidx, Object invalidupper, Object invalidlower, Object 
                          leftincrease, Object rightincrease, Object smooth, Object tight)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx, invalidupper, invalidlower, leftincrease, rightincrease, smooth, tight);
    }


    /// <summary>
    /// Provides the standard 27-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <param name="invalidupper">Input argument #21</param>
    /// <param name="invalidlower">Input argument #22</param>
    /// <param name="leftincrease">Input argument #23</param>
    /// <param name="rightincrease">Input argument #24</param>
    /// <param name="smooth">Input argument #25</param>
    /// <param name="tight">Input argument #26</param>
    /// <param name="tightlb">Input argument #27</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object 
                          upperLimitG, Object lowerLimitG, Object xEndl, Object aMaxl, 
                          Object aMinl, Object xEndr, Object aMaxr, Object aMinr, Object 
                          invalidx, Object invalidupper, Object invalidlower, Object 
                          leftincrease, Object rightincrease, Object smooth, Object 
                          tight, Object tightlb)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx, invalidupper, invalidlower, leftincrease, rightincrease, smooth, tight, tightlb);
    }


    /// <summary>
    /// Provides the standard 28-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <param name="invalidupper">Input argument #21</param>
    /// <param name="invalidlower">Input argument #22</param>
    /// <param name="leftincrease">Input argument #23</param>
    /// <param name="rightincrease">Input argument #24</param>
    /// <param name="smooth">Input argument #25</param>
    /// <param name="tight">Input argument #26</param>
    /// <param name="tightlb">Input argument #27</param>
    /// <param name="tightub">Input argument #28</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object 
                          upperLimitG, Object lowerLimitG, Object xEndl, Object aMaxl, 
                          Object aMinl, Object xEndr, Object aMaxr, Object aMinr, Object 
                          invalidx, Object invalidupper, Object invalidlower, Object 
                          leftincrease, Object rightincrease, Object smooth, Object 
                          tight, Object tightlb, Object tightub)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx, invalidupper, invalidlower, leftincrease, rightincrease, smooth, tight, tightlb, tightub);
    }


    /// <summary>
    /// Provides the standard 29-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <param name="invalidupper">Input argument #21</param>
    /// <param name="invalidlower">Input argument #22</param>
    /// <param name="leftincrease">Input argument #23</param>
    /// <param name="rightincrease">Input argument #24</param>
    /// <param name="smooth">Input argument #25</param>
    /// <param name="tight">Input argument #26</param>
    /// <param name="tightlb">Input argument #27</param>
    /// <param name="tightub">Input argument #28</param>
    /// <param name="minxrange">Input argument #29</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object 
                          upperLimitG, Object lowerLimitG, Object xEndl, Object aMaxl, 
                          Object aMinl, Object xEndr, Object aMaxr, Object aMinr, Object 
                          invalidx, Object invalidupper, Object invalidlower, Object 
                          leftincrease, Object rightincrease, Object smooth, Object 
                          tight, Object tightlb, Object tightub, Object minxrange)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx, invalidupper, invalidlower, leftincrease, rightincrease, smooth, tight, tightlb, tightub, minxrange);
    }


    /// <summary>
    /// Provides the standard 30-input Object interface to the flexWingFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x_in1">Input argument #1</param>
    /// <param name="y">Input argument #2</param>
    /// <param name="weight">Input argument #3</param>
    /// <param name="stationaryPoint">Input argument #4</param>
    /// <param name="tailConcavity">Input argument #5</param>
    /// <param name="smoothCoeff">Input argument #6</param>
    /// <param name="turningPoint_in1">Input argument #7</param>
    /// <param name="boundaryx">Input argument #8</param>
    /// <param name="boundarydx">Input argument #9</param>
    /// <param name="boundarydxx">Input argument #10</param>
    /// <param name="leftright">Input argument #11</param>
    /// <param name="upperLimitG">Input argument #12</param>
    /// <param name="lowerLimitG">Input argument #13</param>
    /// <param name="xEndl">Input argument #14</param>
    /// <param name="aMaxl">Input argument #15</param>
    /// <param name="aMinl">Input argument #16</param>
    /// <param name="xEndr">Input argument #17</param>
    /// <param name="aMaxr">Input argument #18</param>
    /// <param name="aMinr">Input argument #19</param>
    /// <param name="invalidx">Input argument #20</param>
    /// <param name="invalidupper">Input argument #21</param>
    /// <param name="invalidlower">Input argument #22</param>
    /// <param name="leftincrease">Input argument #23</param>
    /// <param name="rightincrease">Input argument #24</param>
    /// <param name="smooth">Input argument #25</param>
    /// <param name="tight">Input argument #26</param>
    /// <param name="tightlb">Input argument #27</param>
    /// <param name="tightub">Input argument #28</param>
    /// <param name="minxrange">Input argument #29</param>
    /// <param name="concave">Input argument #30</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] flexWingFit(int numArgsOut, Object x_in1, Object y, Object weight, 
                          Object stationaryPoint, Object tailConcavity, Object 
                          smoothCoeff, Object turningPoint_in1, Object boundaryx, Object 
                          boundarydx, Object boundarydxx, Object leftright, Object 
                          upperLimitG, Object lowerLimitG, Object xEndl, Object aMaxl, 
                          Object aMinl, Object xEndr, Object aMaxr, Object aMinr, Object 
                          invalidx, Object invalidupper, Object invalidlower, Object 
                          leftincrease, Object rightincrease, Object smooth, Object 
                          tight, Object tightlb, Object tightub, Object minxrange, Object 
                          concave)
    {
      return mcr.EvaluateFunction(numArgsOut, "flexWingFit", x_in1, y, weight, stationaryPoint, tailConcavity, smoothCoeff, turningPoint_in1, boundaryx, boundarydx, boundarydxx, leftright, upperLimitG, lowerLimitG, xEndl, aMaxl, aMinl, xEndr, aMaxr, aMinr, invalidx, invalidupper, invalidlower, leftincrease, rightincrease, smooth, tight, tightlb, tightub, minxrange, concave);
    }


    /// <summary>
    /// Provides an interface for the flexWingFit function in which the input and output
    /// arguments are specified as an array of Objects.
    /// </summary>
    /// <remarks>
    /// This method will allocate and return by reference the output argument
    /// array.<newpara></newpara>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
    /// MM = M(M(:,5)&lt;=0.5, :);
    /// leftright = 1;
    /// IV = MM(MM(:,3)~=0 &amp; MM(:,4)~=0,:);
    /// IV = [IV (IV(:,3)+IV(:,4))./2];
    /// x = IV(:,2)';
    /// y = IV(:,6)';
    /// weight = ones(1,length(x));
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return</param>
    /// <param name= "argsOut">Array of Object output arguments</param>
    /// <param name= "argsIn">Array of Object input arguments</param>
    /// <param name= "varArgsIn">Array of Object representing variable input
    /// arguments</param>
    ///
    [MATLABSignature("flexWingFit", 30, 10, 0)]
    protected void flexWingFit(int numArgsOut, ref Object[] argsOut, Object[] argsIn, params Object[] varArgsIn)
    {
        mcr.EvaluateFunctionForTypeSafeCall("flexWingFit", numArgsOut, ref argsOut, argsIn, varArgsIn);
    }

    /// <summary>
    /// This method will cause a MATLAB figure window to behave as a modal dialog box.
    /// The method will not return until all the figure windows associated with this
    /// component have been closed.
    /// </summary>
    /// <remarks>
    /// An application should only call this method when required to keep the
    /// MATLAB figure window from disappearing.  Other techniques, such as calling
    /// Console.ReadLine() from the application should be considered where
    /// possible.</remarks>
    ///
    public void WaitForFiguresToDie()
    {
      mcr.WaitForFiguresToDie();
    }



    #endregion Methods

    #region Class Members

    private static MWMCR mcr= null;

    private static Exception ex_= null;

    private bool disposed= false;

    #endregion Class Members
  }
}
