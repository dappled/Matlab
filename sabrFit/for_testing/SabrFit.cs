/*
* MATLAB Compiler: 5.1 (R2014a)
* Date: Mon May 18 16:07:41 2015
* Arguments: "-B" "macro_default" "-W" "dotnet:sabrFit,SabrFit,0.0,private" "-T"
* "link:lib" "-d" "C:\Users\Dappled\OneDrive\dappled's
* sky\cs\matlab\convexMonotoneFit\sabrFit\for_testing" "-v" "C:\Program
* Files\MATLAB\R2014a\toolbox\javabuilder\Examples\MagicSquareExample\MagicDemoComp\sabrFi
* t.m" "class{SabrFit:C:\Program
* Files\MATLAB\R2014a\toolbox\javabuilder\Examples\MagicSquareExample\MagicDemoComp\sabrFi
* t.m}" "-a" "C:\Program Files\MATLAB\R2014a\toolbox\optim\optim\lsqnonlin.m" "-a"
* "C:\Program Files\MATLAB\R2014a\toolbox\optim\optim\optimoptions.m" 
*/
using System;
using System.Reflection;
using System.IO;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

#if SHARED
[assembly: System.Reflection.AssemblyKeyFile(@"")]
#endif

namespace sabrFit
{

  /// <summary>
  /// The SabrFit class provides a CLS compliant, MWArray interface to the MATLAB
  /// functions contained in the files:
  /// <newpara></newpara>
  /// C:\Program
  /// Files\MATLAB\R2014a\toolbox\javabuilder\Examples\MagicSquareExample\MagicDemoComp\sa
  /// brFit.m
  /// <newpara></newpara>
  /// deployprint.m
  /// <newpara></newpara>
  /// printdlg.m
  /// </summary>
  /// <remarks>
  /// @Version 0.0
  /// </remarks>
  public class SabrFit : IDisposable
  {
    #region Constructors

    /// <summary internal= "true">
    /// The static constructor instantiates and initializes the MATLAB Compiler Runtime
    /// instance.
    /// </summary>
    static SabrFit()
    {
      if (MWMCR.MCRAppInitialized)
      {
        try
        {
          Assembly assembly= Assembly.GetExecutingAssembly();

          string ctfFilePath= assembly.Location;

          int lastDelimiter= ctfFilePath.LastIndexOf(@"\");

          ctfFilePath= ctfFilePath.Remove(lastDelimiter, (ctfFilePath.Length - lastDelimiter));

          string ctfFileName = "sabrFit.ctf";

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
    /// Constructs a new instance of the SabrFit class.
    /// </summary>
    public SabrFit()
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
    ~SabrFit()
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
    /// Provides a single output, 0-input MWArrayinterface to the sabrFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray sabrFit()
    {
      return mcr.EvaluateFunction("sabrFit", new MWArray[]{});
    }


    /// <summary>
    /// Provides a single output, 1-input MWArrayinterface to the sabrFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="kvalid">Input argument #1</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray sabrFit(MWArray kvalid)
    {
      return mcr.EvaluateFunction("sabrFit", kvalid);
    }


    /// <summary>
    /// Provides a single output, 2-input MWArrayinterface to the sabrFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray sabrFit(MWArray kvalid, MWArray kinvalid)
    {
      return mcr.EvaluateFunction("sabrFit", kvalid, kinvalid);
    }


    /// <summary>
    /// Provides a single output, 3-input MWArrayinterface to the sabrFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray sabrFit(MWArray kvalid, MWArray kinvalid, MWArray y)
    {
      return mcr.EvaluateFunction("sabrFit", kvalid, kinvalid, y);
    }


    /// <summary>
    /// Provides a single output, 4-input MWArrayinterface to the sabrFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray sabrFit(MWArray kvalid, MWArray kinvalid, MWArray y, MWArray wvalid)
    {
      return mcr.EvaluateFunction("sabrFit", kvalid, kinvalid, y, wvalid);
    }


    /// <summary>
    /// Provides a single output, 5-input MWArrayinterface to the sabrFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray sabrFit(MWArray kvalid, MWArray kinvalid, MWArray y, MWArray wvalid, 
                     MWArray winvalid)
    {
      return mcr.EvaluateFunction("sabrFit", kvalid, kinvalid, y, wvalid, winvalid);
    }


    /// <summary>
    /// Provides a single output, 6-input MWArrayinterface to the sabrFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray sabrFit(MWArray kvalid, MWArray kinvalid, MWArray y, MWArray wvalid, 
                     MWArray winvalid, MWArray fwd)
    {
      return mcr.EvaluateFunction("sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd);
    }


    /// <summary>
    /// Provides a single output, 7-input MWArrayinterface to the sabrFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <param name="atfvol">Input argument #7</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray sabrFit(MWArray kvalid, MWArray kinvalid, MWArray y, MWArray wvalid, 
                     MWArray winvalid, MWArray fwd, MWArray atfvol)
    {
      return mcr.EvaluateFunction("sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol);
    }


    /// <summary>
    /// Provides a single output, 8-input MWArrayinterface to the sabrFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <param name="atfvol">Input argument #7</param>
    /// <param name="ttm">Input argument #8</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray sabrFit(MWArray kvalid, MWArray kinvalid, MWArray y, MWArray wvalid, 
                     MWArray winvalid, MWArray fwd, MWArray atfvol, MWArray ttm)
    {
      return mcr.EvaluateFunction("sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol, ttm);
    }


    /// <summary>
    /// Provides a single output, 9-input MWArrayinterface to the sabrFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <param name="atfvol">Input argument #7</param>
    /// <param name="ttm">Input argument #8</param>
    /// <param name="alpha_in1">Input argument #9</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray sabrFit(MWArray kvalid, MWArray kinvalid, MWArray y, MWArray wvalid, 
                     MWArray winvalid, MWArray fwd, MWArray atfvol, MWArray ttm, MWArray 
                     alpha_in1)
    {
      return mcr.EvaluateFunction("sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol, ttm, alpha_in1);
    }


    /// <summary>
    /// Provides a single output, 10-input MWArrayinterface to the sabrFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <param name="atfvol">Input argument #7</param>
    /// <param name="ttm">Input argument #8</param>
    /// <param name="alpha_in1">Input argument #9</param>
    /// <param name="beta">Input argument #10</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray sabrFit(MWArray kvalid, MWArray kinvalid, MWArray y, MWArray wvalid, 
                     MWArray winvalid, MWArray fwd, MWArray atfvol, MWArray ttm, MWArray 
                     alpha_in1, MWArray beta)
    {
      return mcr.EvaluateFunction("sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol, ttm, alpha_in1, beta);
    }


    /// <summary>
    /// Provides a single output, 11-input MWArrayinterface to the sabrFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <param name="atfvol">Input argument #7</param>
    /// <param name="ttm">Input argument #8</param>
    /// <param name="alpha_in1">Input argument #9</param>
    /// <param name="beta">Input argument #10</param>
    /// <param name="nu_in1">Input argument #11</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray sabrFit(MWArray kvalid, MWArray kinvalid, MWArray y, MWArray wvalid, 
                     MWArray winvalid, MWArray fwd, MWArray atfvol, MWArray ttm, MWArray 
                     alpha_in1, MWArray beta, MWArray nu_in1)
    {
      return mcr.EvaluateFunction("sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol, ttm, alpha_in1, beta, nu_in1);
    }


    /// <summary>
    /// Provides a single output, 12-input MWArrayinterface to the sabrFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <param name="atfvol">Input argument #7</param>
    /// <param name="ttm">Input argument #8</param>
    /// <param name="alpha_in1">Input argument #9</param>
    /// <param name="beta">Input argument #10</param>
    /// <param name="nu_in1">Input argument #11</param>
    /// <param name="rho_in1">Input argument #12</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray sabrFit(MWArray kvalid, MWArray kinvalid, MWArray y, MWArray wvalid, 
                     MWArray winvalid, MWArray fwd, MWArray atfvol, MWArray ttm, MWArray 
                     alpha_in1, MWArray beta, MWArray nu_in1, MWArray rho_in1)
    {
      return mcr.EvaluateFunction("sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol, ttm, alpha_in1, beta, nu_in1, rho_in1);
    }


    /// <summary>
    /// Provides a single output, 13-input MWArrayinterface to the sabrFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <param name="atfvol">Input argument #7</param>
    /// <param name="ttm">Input argument #8</param>
    /// <param name="alpha_in1">Input argument #9</param>
    /// <param name="beta">Input argument #10</param>
    /// <param name="nu_in1">Input argument #11</param>
    /// <param name="rho_in1">Input argument #12</param>
    /// <param name="lowerboundvalid">Input argument #13</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray sabrFit(MWArray kvalid, MWArray kinvalid, MWArray y, MWArray wvalid, 
                     MWArray winvalid, MWArray fwd, MWArray atfvol, MWArray ttm, MWArray 
                     alpha_in1, MWArray beta, MWArray nu_in1, MWArray rho_in1, MWArray 
                     lowerboundvalid)
    {
      return mcr.EvaluateFunction("sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol, ttm, alpha_in1, beta, nu_in1, rho_in1, lowerboundvalid);
    }


    /// <summary>
    /// Provides a single output, 14-input MWArrayinterface to the sabrFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <param name="atfvol">Input argument #7</param>
    /// <param name="ttm">Input argument #8</param>
    /// <param name="alpha_in1">Input argument #9</param>
    /// <param name="beta">Input argument #10</param>
    /// <param name="nu_in1">Input argument #11</param>
    /// <param name="rho_in1">Input argument #12</param>
    /// <param name="lowerboundvalid">Input argument #13</param>
    /// <param name="upperboundvalid">Input argument #14</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray sabrFit(MWArray kvalid, MWArray kinvalid, MWArray y, MWArray wvalid, 
                     MWArray winvalid, MWArray fwd, MWArray atfvol, MWArray ttm, MWArray 
                     alpha_in1, MWArray beta, MWArray nu_in1, MWArray rho_in1, MWArray 
                     lowerboundvalid, MWArray upperboundvalid)
    {
      return mcr.EvaluateFunction("sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol, ttm, alpha_in1, beta, nu_in1, rho_in1, lowerboundvalid, upperboundvalid);
    }


    /// <summary>
    /// Provides a single output, 15-input MWArrayinterface to the sabrFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <param name="atfvol">Input argument #7</param>
    /// <param name="ttm">Input argument #8</param>
    /// <param name="alpha_in1">Input argument #9</param>
    /// <param name="beta">Input argument #10</param>
    /// <param name="nu_in1">Input argument #11</param>
    /// <param name="rho_in1">Input argument #12</param>
    /// <param name="lowerboundvalid">Input argument #13</param>
    /// <param name="upperboundvalid">Input argument #14</param>
    /// <param name="lowerboundinvalid">Input argument #15</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray sabrFit(MWArray kvalid, MWArray kinvalid, MWArray y, MWArray wvalid, 
                     MWArray winvalid, MWArray fwd, MWArray atfvol, MWArray ttm, MWArray 
                     alpha_in1, MWArray beta, MWArray nu_in1, MWArray rho_in1, MWArray 
                     lowerboundvalid, MWArray upperboundvalid, MWArray lowerboundinvalid)
    {
      return mcr.EvaluateFunction("sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol, ttm, alpha_in1, beta, nu_in1, rho_in1, lowerboundvalid, upperboundvalid, lowerboundinvalid);
    }


    /// <summary>
    /// Provides a single output, 16-input MWArrayinterface to the sabrFit MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <param name="atfvol">Input argument #7</param>
    /// <param name="ttm">Input argument #8</param>
    /// <param name="alpha_in1">Input argument #9</param>
    /// <param name="beta">Input argument #10</param>
    /// <param name="nu_in1">Input argument #11</param>
    /// <param name="rho_in1">Input argument #12</param>
    /// <param name="lowerboundvalid">Input argument #13</param>
    /// <param name="upperboundvalid">Input argument #14</param>
    /// <param name="lowerboundinvalid">Input argument #15</param>
    /// <param name="upperboundinvalid">Input argument #16</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray sabrFit(MWArray kvalid, MWArray kinvalid, MWArray y, MWArray wvalid, 
                     MWArray winvalid, MWArray fwd, MWArray atfvol, MWArray ttm, MWArray 
                     alpha_in1, MWArray beta, MWArray nu_in1, MWArray rho_in1, MWArray 
                     lowerboundvalid, MWArray upperboundvalid, MWArray lowerboundinvalid, 
                     MWArray upperboundinvalid)
    {
      return mcr.EvaluateFunction("sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol, ttm, alpha_in1, beta, nu_in1, rho_in1, lowerboundvalid, upperboundvalid, lowerboundinvalid, upperboundinvalid);
    }


    /// <summary>
    /// Provides the standard 0-input MWArray interface to the sabrFit MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] sabrFit(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "sabrFit", new MWArray[]{});
    }


    /// <summary>
    /// Provides the standard 1-input MWArray interface to the sabrFit MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="kvalid">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] sabrFit(int numArgsOut, MWArray kvalid)
    {
      return mcr.EvaluateFunction(numArgsOut, "sabrFit", kvalid);
    }


    /// <summary>
    /// Provides the standard 2-input MWArray interface to the sabrFit MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] sabrFit(int numArgsOut, MWArray kvalid, MWArray kinvalid)
    {
      return mcr.EvaluateFunction(numArgsOut, "sabrFit", kvalid, kinvalid);
    }


    /// <summary>
    /// Provides the standard 3-input MWArray interface to the sabrFit MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] sabrFit(int numArgsOut, MWArray kvalid, MWArray kinvalid, MWArray y)
    {
      return mcr.EvaluateFunction(numArgsOut, "sabrFit", kvalid, kinvalid, y);
    }


    /// <summary>
    /// Provides the standard 4-input MWArray interface to the sabrFit MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] sabrFit(int numArgsOut, MWArray kvalid, MWArray kinvalid, MWArray y, 
                       MWArray wvalid)
    {
      return mcr.EvaluateFunction(numArgsOut, "sabrFit", kvalid, kinvalid, y, wvalid);
    }


    /// <summary>
    /// Provides the standard 5-input MWArray interface to the sabrFit MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] sabrFit(int numArgsOut, MWArray kvalid, MWArray kinvalid, MWArray y, 
                       MWArray wvalid, MWArray winvalid)
    {
      return mcr.EvaluateFunction(numArgsOut, "sabrFit", kvalid, kinvalid, y, wvalid, winvalid);
    }


    /// <summary>
    /// Provides the standard 6-input MWArray interface to the sabrFit MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] sabrFit(int numArgsOut, MWArray kvalid, MWArray kinvalid, MWArray y, 
                       MWArray wvalid, MWArray winvalid, MWArray fwd)
    {
      return mcr.EvaluateFunction(numArgsOut, "sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd);
    }


    /// <summary>
    /// Provides the standard 7-input MWArray interface to the sabrFit MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <param name="atfvol">Input argument #7</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] sabrFit(int numArgsOut, MWArray kvalid, MWArray kinvalid, MWArray y, 
                       MWArray wvalid, MWArray winvalid, MWArray fwd, MWArray atfvol)
    {
      return mcr.EvaluateFunction(numArgsOut, "sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol);
    }


    /// <summary>
    /// Provides the standard 8-input MWArray interface to the sabrFit MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <param name="atfvol">Input argument #7</param>
    /// <param name="ttm">Input argument #8</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] sabrFit(int numArgsOut, MWArray kvalid, MWArray kinvalid, MWArray y, 
                       MWArray wvalid, MWArray winvalid, MWArray fwd, MWArray atfvol, 
                       MWArray ttm)
    {
      return mcr.EvaluateFunction(numArgsOut, "sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol, ttm);
    }


    /// <summary>
    /// Provides the standard 9-input MWArray interface to the sabrFit MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <param name="atfvol">Input argument #7</param>
    /// <param name="ttm">Input argument #8</param>
    /// <param name="alpha_in1">Input argument #9</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] sabrFit(int numArgsOut, MWArray kvalid, MWArray kinvalid, MWArray y, 
                       MWArray wvalid, MWArray winvalid, MWArray fwd, MWArray atfvol, 
                       MWArray ttm, MWArray alpha_in1)
    {
      return mcr.EvaluateFunction(numArgsOut, "sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol, ttm, alpha_in1);
    }


    /// <summary>
    /// Provides the standard 10-input MWArray interface to the sabrFit MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <param name="atfvol">Input argument #7</param>
    /// <param name="ttm">Input argument #8</param>
    /// <param name="alpha_in1">Input argument #9</param>
    /// <param name="beta">Input argument #10</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] sabrFit(int numArgsOut, MWArray kvalid, MWArray kinvalid, MWArray y, 
                       MWArray wvalid, MWArray winvalid, MWArray fwd, MWArray atfvol, 
                       MWArray ttm, MWArray alpha_in1, MWArray beta)
    {
      return mcr.EvaluateFunction(numArgsOut, "sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol, ttm, alpha_in1, beta);
    }


    /// <summary>
    /// Provides the standard 11-input MWArray interface to the sabrFit MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <param name="atfvol">Input argument #7</param>
    /// <param name="ttm">Input argument #8</param>
    /// <param name="alpha_in1">Input argument #9</param>
    /// <param name="beta">Input argument #10</param>
    /// <param name="nu_in1">Input argument #11</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] sabrFit(int numArgsOut, MWArray kvalid, MWArray kinvalid, MWArray y, 
                       MWArray wvalid, MWArray winvalid, MWArray fwd, MWArray atfvol, 
                       MWArray ttm, MWArray alpha_in1, MWArray beta, MWArray nu_in1)
    {
      return mcr.EvaluateFunction(numArgsOut, "sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol, ttm, alpha_in1, beta, nu_in1);
    }


    /// <summary>
    /// Provides the standard 12-input MWArray interface to the sabrFit MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <param name="atfvol">Input argument #7</param>
    /// <param name="ttm">Input argument #8</param>
    /// <param name="alpha_in1">Input argument #9</param>
    /// <param name="beta">Input argument #10</param>
    /// <param name="nu_in1">Input argument #11</param>
    /// <param name="rho_in1">Input argument #12</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] sabrFit(int numArgsOut, MWArray kvalid, MWArray kinvalid, MWArray y, 
                       MWArray wvalid, MWArray winvalid, MWArray fwd, MWArray atfvol, 
                       MWArray ttm, MWArray alpha_in1, MWArray beta, MWArray nu_in1, 
                       MWArray rho_in1)
    {
      return mcr.EvaluateFunction(numArgsOut, "sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol, ttm, alpha_in1, beta, nu_in1, rho_in1);
    }


    /// <summary>
    /// Provides the standard 13-input MWArray interface to the sabrFit MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <param name="atfvol">Input argument #7</param>
    /// <param name="ttm">Input argument #8</param>
    /// <param name="alpha_in1">Input argument #9</param>
    /// <param name="beta">Input argument #10</param>
    /// <param name="nu_in1">Input argument #11</param>
    /// <param name="rho_in1">Input argument #12</param>
    /// <param name="lowerboundvalid">Input argument #13</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] sabrFit(int numArgsOut, MWArray kvalid, MWArray kinvalid, MWArray y, 
                       MWArray wvalid, MWArray winvalid, MWArray fwd, MWArray atfvol, 
                       MWArray ttm, MWArray alpha_in1, MWArray beta, MWArray nu_in1, 
                       MWArray rho_in1, MWArray lowerboundvalid)
    {
      return mcr.EvaluateFunction(numArgsOut, "sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol, ttm, alpha_in1, beta, nu_in1, rho_in1, lowerboundvalid);
    }


    /// <summary>
    /// Provides the standard 14-input MWArray interface to the sabrFit MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <param name="atfvol">Input argument #7</param>
    /// <param name="ttm">Input argument #8</param>
    /// <param name="alpha_in1">Input argument #9</param>
    /// <param name="beta">Input argument #10</param>
    /// <param name="nu_in1">Input argument #11</param>
    /// <param name="rho_in1">Input argument #12</param>
    /// <param name="lowerboundvalid">Input argument #13</param>
    /// <param name="upperboundvalid">Input argument #14</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] sabrFit(int numArgsOut, MWArray kvalid, MWArray kinvalid, MWArray y, 
                       MWArray wvalid, MWArray winvalid, MWArray fwd, MWArray atfvol, 
                       MWArray ttm, MWArray alpha_in1, MWArray beta, MWArray nu_in1, 
                       MWArray rho_in1, MWArray lowerboundvalid, MWArray upperboundvalid)
    {
      return mcr.EvaluateFunction(numArgsOut, "sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol, ttm, alpha_in1, beta, nu_in1, rho_in1, lowerboundvalid, upperboundvalid);
    }


    /// <summary>
    /// Provides the standard 15-input MWArray interface to the sabrFit MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <param name="atfvol">Input argument #7</param>
    /// <param name="ttm">Input argument #8</param>
    /// <param name="alpha_in1">Input argument #9</param>
    /// <param name="beta">Input argument #10</param>
    /// <param name="nu_in1">Input argument #11</param>
    /// <param name="rho_in1">Input argument #12</param>
    /// <param name="lowerboundvalid">Input argument #13</param>
    /// <param name="upperboundvalid">Input argument #14</param>
    /// <param name="lowerboundinvalid">Input argument #15</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] sabrFit(int numArgsOut, MWArray kvalid, MWArray kinvalid, MWArray y, 
                       MWArray wvalid, MWArray winvalid, MWArray fwd, MWArray atfvol, 
                       MWArray ttm, MWArray alpha_in1, MWArray beta, MWArray nu_in1, 
                       MWArray rho_in1, MWArray lowerboundvalid, MWArray upperboundvalid, 
                       MWArray lowerboundinvalid)
    {
      return mcr.EvaluateFunction(numArgsOut, "sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol, ttm, alpha_in1, beta, nu_in1, rho_in1, lowerboundvalid, upperboundvalid, lowerboundinvalid);
    }


    /// <summary>
    /// Provides the standard 16-input MWArray interface to the sabrFit MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="kvalid">Input argument #1</param>
    /// <param name="kinvalid">Input argument #2</param>
    /// <param name="y">Input argument #3</param>
    /// <param name="wvalid">Input argument #4</param>
    /// <param name="winvalid">Input argument #5</param>
    /// <param name="fwd">Input argument #6</param>
    /// <param name="atfvol">Input argument #7</param>
    /// <param name="ttm">Input argument #8</param>
    /// <param name="alpha_in1">Input argument #9</param>
    /// <param name="beta">Input argument #10</param>
    /// <param name="nu_in1">Input argument #11</param>
    /// <param name="rho_in1">Input argument #12</param>
    /// <param name="lowerboundvalid">Input argument #13</param>
    /// <param name="upperboundvalid">Input argument #14</param>
    /// <param name="lowerboundinvalid">Input argument #15</param>
    /// <param name="upperboundinvalid">Input argument #16</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] sabrFit(int numArgsOut, MWArray kvalid, MWArray kinvalid, MWArray y, 
                       MWArray wvalid, MWArray winvalid, MWArray fwd, MWArray atfvol, 
                       MWArray ttm, MWArray alpha_in1, MWArray beta, MWArray nu_in1, 
                       MWArray rho_in1, MWArray lowerboundvalid, MWArray upperboundvalid, 
                       MWArray lowerboundinvalid, MWArray upperboundinvalid)
    {
      return mcr.EvaluateFunction(numArgsOut, "sabrFit", kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol, ttm, alpha_in1, beta, nu_in1, rho_in1, lowerboundvalid, upperboundvalid, lowerboundinvalid, upperboundinvalid);
    }


    /// <summary>
    /// Provides an interface for the sabrFit function in which the input and output
    /// arguments are specified as an array of MWArrays.
    /// </summary>
    /// <remarks>
    /// This method will allocate and return by reference the output argument
    /// array.<newpara></newpara>
    /// M-Documentation:
    /// clc
    /// M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
    /// i = find(M(:,1)' == 1);
    /// iinvalid = find(M(:,1)' ~= 1);
    /// atfvol = 0;
    /// alpha = 0.25;
    /// beta = 0.5;
    /// nu = 0.3;
    /// rho = 0.7;
    /// kvalid = M(i,2)';
    /// kinvalid = M(iinvalid, 2)';
    /// y = M(i,8)';
    /// wvalid = M(i,7)';
    /// winvalid = M(iinvalid, 7)';
    /// ttm = 2.011;
    /// fwd = 190.8512;
    /// upperBoundvalid = M(i,9)';  inf(1,n);
    /// upperBoundvalid(upperBoundvalid == 1) = inf;
    /// upperBoundinvalid = M(iinvalid,9)';  inf(1,n);
    /// upperBoundinvalid(upperBoundinvalid == 1) = inf;
    /// lowerBoundvalid = M(i,10)';  zeros(1,n);
    /// lowerBoundinvalid = M(iinvalid, 10)';
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return</param>
    /// <param name= "argsOut">Array of MWArray output arguments</param>
    /// <param name= "argsIn">Array of MWArray input arguments</param>
    ///
    public void sabrFit(int numArgsOut, ref MWArray[] argsOut, MWArray[] argsIn)
    {
      mcr.EvaluateFunction("sabrFit", numArgsOut, ref argsOut, argsIn);
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
