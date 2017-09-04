
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using DataService;
using System.Text;
using System.Web.UI.WebControls;
using System.Threading;

partial class Patient : System.Web.UI.Page
{
    public int CalcVal0_id;

    public string strStatinPublic;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        CVDevaluator.SharedFunctions.LoginRedirect();
        if (!Page.IsPostBack)
        {
            if ((Request.QueryString["id"] != null))
            {
                CalcVal0_id = Convert.ToInt32(Request.QueryString["id"]);
                Fill_CalcVal0(CalcVal0_id);
                btnCompute_Click(null, null);
            }
        }
        else
        {
        }
    }

    protected int Val_int(string str)
    {
        if (str.Trim() == "")
            return 0;
        else
        {
            try
            {
                double tmp = Convert.ToDouble(str);
                return Convert.ToInt32(Math.Round(tmp));
            }
            catch
            {
                return 0;
            }
        }
    }

    protected double Val_dbl(string str)
    {
        if (str.Trim() == "")
            return 0;
        else
        {
            try
            {
                return Convert.ToDouble(str);
            }
            catch
            {
                return 0;
            }
        }
    }

    protected void btnCompute_Click(object sender, System.EventArgs e)
    {
        Thread t = new Thread(btnComputeThread, 4194304); // 4M of stack size
        t.Start();
        t.Join();
    }

    protected void btnComputeThread()
    {
        if (!(string.IsNullOrEmpty(txtCholesterol.Text.Trim()) & string.IsNullOrEmpty(txtTRG.Text.Trim()) & string.IsNullOrEmpty(txtLDL.Text.Trim()) & string.IsNullOrEmpty(txtHDL.Text.Trim())))
        {
            bool needtoreturn = false;
            if (string.IsNullOrEmpty(txtCholesterol.Text.Trim()))
            {
                CustomValidator err = new CustomValidator();
                err.Text = "*";
                err.IsValid = false;
                err.ErrorMessage = "Please Enter CHOLESTEROL";
                err.ControlToValidate = "txtCholesterol";
                Page.Validators.Add(err);
                needtoreturn = true;
            }

            if (string.IsNullOrEmpty(txtTRG.Text.Trim()))
            {
                CustomValidator err = new CustomValidator();
                err.Text = "*";
                err.IsValid = false;
                err.ErrorMessage = "Please Enter TRG";
                err.ControlToValidate = "txtTRG";
                Page.Validators.Add(err);
                needtoreturn = true;
            }

            if (Val_int(txtTRG.Text) <= 400)
            {
                if (string.IsNullOrEmpty(txtLDL.Text.Trim()))
                {
                    CustomValidator err = new CustomValidator();
                    err.Text = "*";
                    err.IsValid = false;
                    err.ErrorMessage = "Please Enter LDL-C";
                    err.ControlToValidate = "txtLDL";
                    Page.Validators.Add(err);
                    needtoreturn = true;
                }
            }

            if (string.IsNullOrEmpty(txtHDL.Text.Trim()))
            {
                CustomValidator err = new CustomValidator();
                err.Text = "*";
                err.IsValid = false;
                err.ErrorMessage = "Please Enter HDL-C";
                err.ControlToValidate = "txtHDL";
                Page.Validators.Add(err);
                needtoreturn = true;
            }

            if (needtoreturn)
            {
                return;
            }
        }

        if (!string.IsNullOrEmpty(txtCHF.Text.Trim()) & !string.IsNullOrEmpty(TxtHF.Text.Trim()))
        {
            double tmp1 = Val_dbl(txtCHF.Text);
            double tmp2 = Val_dbl(TxtHF.Text);
            if ((tmp1 != tmp2))
            {
                CustomValidator err = new CustomValidator();
                err.Text = "*";
                err.IsValid = false;
                err.ErrorMessage = "Please Enter same value for LVEFs";
                err.ControlToValidate = "txtCHF";
                Page.Validators.Add(err);
                return;
            }
        }

        if (!string.IsNullOrEmpty(txtCHF.Text.Trim()) & !string.IsNullOrEmpty(txtEF.Text.Trim()))
        {
            double tmp1 = Val_dbl(txtCHF.Text);
            double tmp2 = Val_dbl(txtEF.Text);
            if ((tmp1 != tmp2))
            {
                CustomValidator err = new CustomValidator();
                err.Text = "*";
                err.IsValid = false;
                err.ErrorMessage = "Please Enter same value for LVEFs";
                err.ControlToValidate = "txtCHF";
                Page.Validators.Add(err);
                return;
            }
        }

        if (!string.IsNullOrEmpty(TxtHF.Text.Trim()) & !string.IsNullOrEmpty(txtEF.Text.Trim()))
        {
            double tmp1 = Val_dbl(TxtHF.Text);
            double tmp2 = Val_dbl(txtEF.Text);
            if ((tmp1 != tmp2))
            {
                CustomValidator err = new CustomValidator();
                err.Text = "*";
                err.IsValid = false;
                err.ErrorMessage = "Please Enter same value for LVEFs";
                err.ControlToValidate = "TxtHF";
                Page.Validators.Add(err);
                return;
            }
        }

        if (!string.IsNullOrEmpty(txtTRG.Text.Trim()))
        {
            if (Val_dbl(txtTRG.Text) > 400)
            {
                txtLDL.Text = "0";
            }
        }


        int intTestAge = Val_int(txtAge.Text);
        int intTestGender = Val_int(rblGender.SelectedValue);
        int intweight = Val_int(txtWeight.Text);
        int intTestWaist = Val_int(txtWaist.Text);
        int intTestBMI = Val_int(txtBMI.Text);
        bool blnTestAsianCode = chkAsianCode.Checked;
        int intTestSBP = Val_int(txtSBP.Text);
        bool blnSymptomatic = chkSymptomatic.Checked;
        bool blnpreg = chkpreg.Checked;
        bool blnstatint = chkstatint.Checked;
        //int intOrthostaticSBP = Val_int(txtOrthostaticSBP.Text);
        int intTestDBP = Val_int(txtDBP.Text);
        int intTestHR = Val_int(txtHR.Text);
        int intTestADBP = Val_int(txtADBP.Text);
        int intTestASBP = Val_int(txtASBP.Text);
        int intNameq = Val_int(txtNameq.Text);
        double dblTestHomo = Val_dbl(txtHomo.Text);
        double dblCreatinine = Val_dbl(txtcreatinine.Text);
        int intTestGlucose = Val_int(txtGlucose.Text);
        int intTestBUN = Val_int(txtBUN.Text);
        int intTestGFR = 0;
        if ((string.IsNullOrEmpty(txtGFR.Text.Trim())))
        {
            intTestGFR = 120;
        }
        else
        {
            intTestGFR = Val_int(txtGFR.Text);
        }
        int intOrthostaticSBP = 0;
        if ((string.IsNullOrEmpty(txtOrthostaticSBP.Text.Trim())))
        {
            intOrthostaticSBP = intTestSBP-1;
        }
        else
        {
            intOrthostaticSBP = Val_int(txtOrthostaticSBP.Text);
        }
        int inturineNa = 0;
        int intuosm = 0;
        int intsosm = 0;
        if (intNameq < 130)
        {
            inturineNa = Val_int(txturineNa.Text);
            intuosm = Val_int(txtuosm.Text);
            intsosm = Val_int(txtsosm.Text);
        }
        else
        {
            inturineNa = 0;
            intuosm = 0;
            intsosm = 0;
        }


        int intTestCholesterol = Val_int(txtCholesterol.Text);
        int intTestTRG = Val_int(txtTRG.Text);
        int intTestLDL = Val_int(txtLDL.Text);
        int intTestHDL = Val_int(txtHDL.Text);
        int intApoB = Val_int(txtApoB.Text);
        int intLDLP = Val_int(txtLDLP.Text);
        int intTestLPA = Val_int(txtLPA.Text);
        bool blnalready = chkalready.Checked;
        double dblASCVD = Val_dbl(txtASCVD.Text);

        double dblTestHBA1C = Val_dbl(txtHBA1C.Text);
        int intCRP = Val_int(txtCRP.Text);
        

        bool blnTestEKG = chkEKG.Checked;
        bool blnDTS = chkDTS.Checked;
        bool blnEcho = chkEcho.Checked;
        bool blnchestxray = chkchestxray.Checked;
        bool blnHC = ChkHC.Checked;
        int intTestCCS = Val_int(txtCCS.Text);
        int intTestTotalCCS = Val_int(txtTotalCCS.Text);
        int intpalseconds = Val_int(txtpalseconds.Text);
        int intpalminutes = Val_int(txtpalminutes.Text);
        int intpalhours = Val_int(txtpalhours.Text);
        int intQRS = 0;
        int intQRSAx = 0;
        int intQTc = 0;
        int intPRduration = 0;
        bool blnST = false;
        bool blnTWI = false;
        bool blnnonspesific = false;
        bool blnlowv = false;
        bool blnLVH = false;
        bool blnFreqVentPac = false;
        bool blnWPW = false;
        bool blnatrialfib = false;
        bool blnLBBB = false;
        bool blnabnormq = false;
        bool blninfreqvent = false;
        bool blnAPCs = false;
        bool blnJPDs = false;
        bool blnPSVT = false;
        bool blnRBBB = false;
        
        bool blnIVCD = false;
        bool blnNSR = false;
        bool blnVpace = false;
        bool blnflutter = false;
        bool blnfirstdegree = false;
        bool blnsecondegree = false;
        bool blnthirdegree = false;
        bool blntype1 = false;
        bool blntype2 = false;
        bool blnLVeccenticity = false;
        bool blntypicalAF = false;
        bool blnatypicalAF = false;

        bool blnpericardial = false;
        bool blnRWMA = false;
        bool blnMPI = false;
        bool blnEnlargedLA = false;
        int intMP = 0;
        bool blnIVSshift = false;
        bool blnmitralE = false;
        bool blnhepatic = false;
        bool blnEnlargedRA = false;
        bool blnEnlargedRV = false;
        bool blnmidsystolicn = false;
        double dblTRJ = 0;

        if (chkEKG.Checked == true)
        {
            intQRS = Val_int(txtQRS.Text);
            intQRSAx = Val_int(txtQRSAx.Text);
            intQTc = Val_int(txtQTc.Text);
            blnST = ChkST.Checked;
            blnTWI = ChkTWI.Checked;
            blnnonspesific = Chknonspesific.Checked;
            blnlowv = Chklowv.Checked;
            intPRduration = Val_int(txtPRduration.Text);
            blnabnormq = Chkabnormq.Checked;
            blnFreqVentPac = ChkFreqVentPac.Checked;
            blnWPW = ChkWPW.Checked;
            blnLBBB = ChkLBBB.Checked;
            blnLVH = ChkLVH.Checked;
            blninfreqvent = Chkinfreqvent.Checked;
            blnAPCs = ChkAPCs.Checked;
            blnJPDs = ChkJPDs.Checked;
            blnPSVT = ChkPSVT.Checked;
            blnRBBB = ChKRBBB.Checked;
            blnNSR = ChkNSR.Checked;
            blnVpace = ChkVPace.Checked;
            blnIVCD = ChkIVCD.Checked;
            blnatrialfib = Chkatrialfib.Checked;
            blnflutter = Chkflutter.Checked;
            blnfirstdegree = Chkfirstdegree.Checked;
            blnsecondegree = Chksecondegree.Checked;
            blnthirdegree = Chkthirdegree.Checked;
        }
        else
        {
            intQRS = 0;
            intQRSAx = 0;
            intQTc = 0;
            blnST = false;
            blnTWI = false;
            blnnonspesific = false;
            blnlowv = false;
            intPRduration = 0;
            blnFreqVentPac = false;
            blnWPW = false;
            blnLBBB = false;
            blnLVH = false;
            blnabnormq = false;
            blninfreqvent = false;
            blnAPCs = false;
            blnJPDs = false;
            blnPSVT = false;
            blnRBBB = false;
            blnNSR = false;
            blnVpace = false;
            blnIVCD = false;
            blnatrialfib = false;
            blnflutter = false;
            blnfirstdegree = false;
            blnsecondegree = false;
            blnthirdegree = false;
        }

        if (blnflutter)
        {
            blntypicalAF = chktypicalAF.Checked;
            blnatypicalAF = chkatypicalAF.Checked;
        }
        else
        {
            blntypicalAF = false;
            blnatypicalAF = false;
        }
        if (blnsecondegree)
        {
            blntype1 = Chktype1.Checked;
            blntype2 = Chktype2.Checked;
        }
        else
        {
            blntype1 = false;
            blntype2 = false;
        }

        bool blnbifascicular = false;
        bool blnLAHB = false;
        bool blnLPHB = false;

        if (blnLBBB)
        {
            blnbifascicular = chkbifascicular.Checked;
            blnLAHB = chkLAHB.Checked;
            blnLPHB = chkLPHB.Checked;
        }
        else
        {
            blnbifascicular = false;
            blnLAHB = false;
            blnLPHB = false;
        }

        if (chkEcho.Checked == true)
        {
            blnpericardial = chkpericardial.Checked;
            blnRWMA = chkRWMA.Checked;
            blnLVeccenticity = chkLVeccenticity.Checked;
            blnMPI = chkMPI.Checked;
            blnEnlargedLA = chkEnlargedLA.Checked;
            intMP = Val_int(txtMP.Text);
            blnIVSshift = ChkIVSshift.Checked;
            blnmitralE = ChkmitralE.Checked;
            blnhepatic = Chkhepatic.Checked;
            blnEnlargedRA = ChkEnlargedRA.Checked;
            blnEnlargedRV = ChkEnlargedRV.Checked;
            blnmidsystolicn = Chkmidsystolicn.Checked;
            dblTRJ = Val_dbl(txtTRJ.Text);
        }
        else
        {
            blnpericardial = false;
            blnRWMA = false;
            blnLVeccenticity = false;
            blnMPI = false;
            blnEnlargedLA = false;
            intMP = 0;
            blnIVSshift = false;
            blnmitralE = false;
            blnhepatic = false;
            blnEnlargedRA = false;
            blnEnlargedRV = false;
            blnmidsystolicn = false;
            dblTRJ = 0;
        }

        bool blnRPLE = false;
        bool blnLPLE = false;
        bool blnpuledema = false;
        bool blnenlarcar = false;
        bool blnWidened = false;
        bool blnpneumothorax = false;
        bool blninfiltrate = false;
        bool blnconsolidation= false;

        if (blnchestxray)
        {
            blnRPLE = chkRPLE.Checked;
            blnLPLE = chkLPLE.Checked;
            blnpuledema = chkpuledema.Checked;
            blnenlarcar = chkenlarcar.Checked;
            blnWidened = chkWidened.Checked;
            blnpneumothorax = chkpneumothorax.Checked;
            blninfiltrate = chkinfiltrate.Checked;
            blnconsolidation = chkconsolidation.Checked;
        }
        else
        {
            blnRPLE = false;
            blnLPLE = false;
            blnpuledema = false;
            blnenlarcar = false;
            blnWidened = false;
            blnpneumothorax = false;
            blninfiltrate = false;
            blnconsolidation = false;
        }



        //Dim intTTS As Integer
        int intStressSummedScore = 0;
        int intExTimeMin = 0;
        int intMETS = Val_int(TxtMETS.Text);
        {
            if (Chkunab.Checked == true)
            {
                intMETS = 3;
            }
        }

        int intDASI = 0;
        int intDSE = 0;
        int intreversible = 0;
        int intWMA = 0;
        int intMPS = 0;
        int intMaxST = 0;
        int intAnginaIndex = 0;
        bool blnSTelevation = false;
        bool blnabnormalBP = false;
        bool blnprolongedST = false;
        bool blnExVT = false;
        bool blnTID = false;
        bool blnfixed = false;
        bool blnviable = false;

        bool blnunterpret = false;
        bool blnnewWMA = false;
        int intTTS = 0;

        if (blnDTS == true)
        {
            blnSTelevation = ChkSTelevation.Checked;
            blnabnormalBP = ChkabnormalBP.Checked;
            blnExVT = ChkExVT.Checked;
            blnprolongedST = ChkprolongedST.Checked;
            blnTID = chkTID.Checked;
            blnfixed = Chkfixed.Checked;
            blnunterpret = Chkuninterpret.Checked;

            intTTS = Val_int(TxtTTS.Text);
            intExTimeMin = Val_int(txtExTimeMin.Text);
            intMaxST = Val_int(txtMaxST.Text);


            intDASI = Val_int(TxtDASI.Text);
            if (string.IsNullOrEmpty(txtDSE.Text.Trim()))
            {
                intDSE = -999;
            }
            else
            {
                intDSE = Val_int(txtDSE.Text);
            }
            if (string.IsNullOrEmpty(txtreversible.Text.Trim()))
            {
                intreversible = -999;
            }
            else
            {
                intreversible = Val_int(txtreversible.Text);
            }
            if (string.IsNullOrEmpty(txtWMA.Text.Trim()))
            {
                intWMA = -999;
            }
            else
            {
                intWMA = Val_int(txtWMA.Text);
            }
            if (string.IsNullOrEmpty(txtMPS.Text.Trim()))
            {
                intMPS = -999;
            }
            else
            {
                intMPS = Val_int(txtMPS.Text);
            }
            if (string.IsNullOrEmpty(txtStressSummedScore.Text.Trim()))
            {
                intStressSummedScore = -999;
            }
            else
            {
                intStressSummedScore = Val_int(txtStressSummedScore.Text);
            }


            if (rbAnginaIndex.SelectedIndex > -1)
            {
                intAnginaIndex = Val_int(rbAnginaIndex.Text);
            }
            else
            {
                intAnginaIndex = -1;
            }
        }
        else
        {
            intTTS = 0;
            blnSTelevation = false;
            blnabnormalBP = false;
            blnExVT = false;
            blnprolongedST = false;
            blnTID = false;
            blnfixed = false;
            blnunterpret = false;

            intExTimeMin = 0;
            intMaxST = 0;
            intStressSummedScore = 0;
            intDASI = 0;
            intDSE = 0;
            intreversible = 0;
            intWMA = 0;
            intMPS = 0;
            intAnginaIndex = -1;
        }

        if (blnfixed == true)
        {
            blnviable = Chkviable.Checked;
        }
        else
        {
            blnviable = false;
        }

        bool blnTestHF = false;
        int intdur = Val_int(txtdur.Text);
        bool blnnewHF = ChknewHF.Checked;
        bool blnD1 = ChkD1.Checked;
        bool blnD2 = ChkD2.Checked;
        bool blnD3 = ChkD3.Checked;
        bool blnD4 = ChkD4.Checked;
        bool blnD5 = ChkD5.Checked;
        //Dim blnD6 As Boolean = ChkD6.Checked
        bool blnHFPLVF = ChKHFPLVF.Checked;
        bool blnlung = Chklung.Checked;
        int intFEVP = Val_int(txtFEVP.Text);
        int intFVCP = Val_int(txtFVCP.Text);
        int intDLCO = Val_int(txtDLCO.Text);
        double dblFEV1 = Val_dbl(txtFEV1.Text);
        bool blnfoutpt = Chkfoutpt.Checked;
        bool blnICD = chkICD.Checked;

        bool blnTestNonischemicCMP = ChkNonischemicCMP.Checked;
        bool blnTestischemic = ChkIschemic.Checked;
        bool blnTestIschemicCMP = ChkIschemicCMP.Checked;
        bool blnTestMI = chkMI.Checked;

        bool blnLVaneurysm = false;
        if (blnTestMI)
        {
            blnLVaneurysm = chkLVaneurysm.Checked;
        }
        else
        {
            blnLVaneurysm = false;
        }

        bool blnFamHistSuddenDeath = false;
        bool blnLVHGreater30 = false;
        bool blnAbnormBP = false;
        bool blnLVOT = false;
        if (blnTestIschemicCMP)
        {
            blnFamHistSuddenDeath = ChkFamHistSuddenDeath.Checked;
            blnLVHGreater30 = ChkLVHGreater30.Checked;
            blnAbnormBP = ChkAbnormBP.Checked;
            blnLVOT = ChkLVOT.Checked;
        }
        else
        {
            blnFamHistSuddenDeath = false;
            blnLVHGreater30 = false;
            blnAbnormBP = false;
            blnLVOT = false;
        }

        bool checkCHFetyology = false;

        bool blnCardiotoxins = ChkCardiotoxins.Checked;
        bool blnFamHXofCMP = ChkFamHXofCMP.Checked;
        bool blnmyocarditis = Chkmyocarditis.Checked;
        bool blnvalvular = Chkvalvular.Checked;
        bool blnSuspectedRVDysplasia = ChkSuspectedRVDysplasia.Checked;
        bool blnrestrictive = Chkrestrictive.Checked;

        if ( blnHFPLVF || blnTestMI || blnTestNonischemicCMP || blnTestischemic || blnTestIschemicCMP || blnCardiotoxins || blnFamHXofCMP || blnSuspectedRVDysplasia || blnmyocarditis || blnrestrictive || intdur > 0 || blnRPLE)
        {
            blnTestHF = true;
        }
        else if (!string.IsNullOrEmpty(txtdur.Text.Trim()))
        {
            checkCHFetyology = true;
        }
        else if (!string.IsNullOrEmpty(txtCHF.Text.Trim()))
        {
            checkCHFetyology = true;
        }
        else
        {
            blnTestHF = false;
        }

        int intTestCHF = 0;

        if (string.IsNullOrEmpty(TxtHF.Text.Trim()))
        {
            intTestCHF = Val_int(txtCHF.Text);
        }
        else if (intTestCHF < 40 & intTestCHF > 0)
        {
            blnTestHF = true;
        }
        int intO2Sat = Val_int(txtO2sat.Text);
        int intproBNP = Val_int(txtproBNP.Text);

        int intBNP = Val_int(txtBNP.Text);
        if (intBNP == 0 & intproBNP > 0)
        {
            intBNP = intproBNP / 5;
        }

        bool blnweightgain = chkweightgain.Checked;
        bool blnOSA = ChkOSA.Checked;
        bool blncough = Chkcough.Checked;
        bool blnsputum = Chksputum.Checked;
        bool blnsinus = Chksinus.Checked;
        bool blnpalpitations = Chkpalpitations.Checked;
        bool blnorthopnea = Chkorthopnea.Checked;
        bool blnPND = ChkPND.Checked;
        bool blnPEd = ChkPEd.Checked;
        bool blnthorax = chkthorax.Checked;
        bool blnrhonchi = chkrhonchi.Checked;
        bool blnpercussion = chkpercussion.Checked;
        bool blnS3 = ChkS3.Checked;
        bool blnPMI = ChkPMI.Checked;
        bool blnshock = Chkshock.Checked;
        bool blnskinwarm = Chkskinwarm.Checked;
        bool blnDOE = ChkDOE.Checked;

        bool blnDysp = Chkdysp.Checked;
        bool blnMR = ChkMR.Checked;
        bool blnS4 = ChkS4.Checked;
        bool blnbruit = Chkbruit.Checked;
        bool blnascites = Chkascites.Checked;
        bool blnHJR = ChkHJR.Checked;
        bool blndistant = Chkdistant.Checked;
        bool blnHepato = Chkhepato.Checked;
        bool blnweight = ChkWeight.Checked;
        bool blnJVD = ChkJVD.Checked;
        bool blnEdema = Chkedema.Checked;
        bool blnAMS = ChkAMS.Checked;
        bool blnWG = ChkWG.Checked;
        double dblcr = 0;
        double dblcrinc = 0;
        double dblurine = 0;
        if (blnWG)
        {
            dblcr = Val_dbl(txtcr.Text);
            dblcrinc = Val_dbl(txtcrinc.Text);
            dblurine = Val_dbl(txturine.Text);
        }
        else
        {
            dblcr = 0;
            dblcrinc = 0;
            dblurine = 0;
        }
        bool blnlocaledema = chklocaledema.Checked;
        bool blndiffusedema = chkdiffusedema.Checked;
        bool blnpitting = chkpitting.Checked;
        bool blnnonpitting = chknonpitting.Checked;
        bool blnmalaise= chkmalaise.Checked;
        bool blnAbsharp= chkAbsharp.Checked;
        bool blnAbcolic = chkAbcolic.Checked;
        bool blnAbaching = chkAbaching.Checked;
        bool blnAbspasm = chkAbspasm.Checked;


        bool blnlocalized = chklocalized.Checked;
        bool blndiffuse = chkdiffuse.Checked;
        bool blnumbilical = chkperiumbilical.Checked;
        bool blnepigastric = chkepigastric.Checked;
       
        bool blnsuprapubic = chksuprapubic.Checked;
        bool blndistention = chkdistention.Checked;
        bool blnrebound = chkrebound.Checked;
        bool blnpulsatile = chkpulsatile.Checked;
        bool blnbowelsounds = chkbowelsounds.Checked;
        bool blnpsaos = chkpsaos.Checked;
        bool blnMcburney = chkMcburney.Checked;
        bool blnRovsing = chkRovsing.Checked;
        bool blnMurphy = chkMurphy.Checked;
        bool blnobturator = chkobturator.Checked;
        bool blncarnett= chkcarnett.Checked;
        bool blncostovertebral = chkcostovertebral.Checked;
        bool blnskin = chkskin.Checked;
        bool blnvomiting = chkvomiting.Checked;
        bool blnanorexia = chkanorexia.Checked;
        bool blnbowel = chkbowel.Checked;
        bool blnpostoperative = chkpostoperative.Checked;
        bool blnfamilyhistoryAA = chkfamilyhistoryAA.Checked;
        bool blnurinary = chkurinary.Checked;
        bool blnobgyn = chkobgyn.Checked;
        bool blnhematuria = chkhematuria.Checked;
        bool blnechymosis = false;
        bool blncelulitis = false;
        bool blnherpes = false;
        bool blnhenoch = false;
        bool blnicterus = chkicterus.Checked;
        bool blncaput = false;

        bool blnvomitfirst = false;
        bool blnpainfirst =false;
        bool blnpalday = chkpalday.Checked;
        bool blnpalweek = chkpalweek.Checked;
        bool blnpalmonth = chkpalmonth.Checked;
        bool blnpalyear = chkpalyear.Checked;
        bool blnextrasys = chkextrasys.Checked;
        bool blnanxiety = chkanxiety.Checked;
        bool blntachycardic = chktachycardic.Checked;
        bool blnvagalabrupt = false;
        bool blnvagalgradual = false;
        bool blnvagalnoresponse = false;

        if (blntachycardic)
        {
            blnvagalabrupt = chkvagalabrupt.Checked;
            blnvagalgradual = chkvagalgradual.Checked;
            blnvagalnoresponse = chkvagalnoresponse.Checked;
        }
        else
        {
            blnvagalabrupt = false;
            blnvagalgradual = false;
            blnvagalnoresponse = false;
        }

        bool blnpulsation = chkpulsation.Checked;
        bool blnpalsyncope = chkpalsyncope.Checked;
        bool blnpalangina = chkpalangina.Checked;
        bool blnpalneck = chkpalneck.Checked;
        bool blnpalpolyuria = chkpalpolyuria.Checked;
        bool blnpalposition = chkpalposition.Checked;
        bool blnpalsudden = chkpalsudden.Checked;
        bool blnpalexertion = chkpalexertion.Checked;
        bool blnpalpostexertion = chkpalpostexertion.Checked;
        bool blnpalNSR = chkpalNSR.Checked;
        bool blnpalregRR = chkpalregRR.Checked;
        bool blnpalirregRR = chkpalirregRR.Checked;
       

        bool blnhypovolemia = chkhypovolemia.Checked;
        bool blnallergen = chkallergen.Checked;
        bool blninfectious = chkinfectious.Checked;
        bool blnpharmacologic = chkpharmacologic.Checked;
        bool blnspinalcord = chkspinalcord.Checked;
        bool blncentral = false;
        bool blnperipheral = false;
        bool blnCyanosis = chkCyanosis.Checked;
        if (blnCyanosis)
        {
           blncentral = chkcentral.Checked;
           blnperipheral = chkperipheral.Checked;

        }
        else
        {
            blncentral = false;
            blnperipheral = false;

        }
        
        if (blnskin)
        {
            blnechymosis = chkechymosis.Checked;
            blncaput = chkcaput.Checked;
            blnherpes = chkherpes.Checked;
            blncelulitis = chkcelulitis.Checked;
            blnhenoch= chkhenoch.Checked;
           
        }
        else
        {
            blnechymosis = false;
            blncaput = false;
            blnherpes = false;
            blncelulitis = false;
            blnhenoch = false;
           
        }
        if (blnvomiting)
        {
            blnvomitfirst = chkvomitfirst.Checked;
            blnpainfirst = chkpainfirst.Checked;
           
        }
        else
        {
            blnvomitfirst = false;
            blnpainfirst = false;
            
        }



        bool blnTestA = chkA.Checked;
        bool blnTestB = chkB.Checked;
        bool blnTestC = chkC.Checked;
        bool blnTestD = chkD.Checked;

        bool blncom = false;
        int intVO2 = 0;

        if (blnTestD == true)
        {
            blncom = Chkcom.Checked;
            intVO2 = Val_int(TxtVO2.Text);
        }
        else
        {
            blncom = false;
            intVO2 = 0;
        }

        bool blnTestNYHA1 = ChkNYHA1.Checked || ChkNYHA1_2.Checked;
        bool blnTestNYHA2 = ChkNYHA2.Checked || ChkNYHA2_2.Checked;
        bool blnTestNYHA3 = ChkNYHA3.Checked || ChkNYHA3_2.Checked;
        bool blnTestNYHA4 = ChkNYHA4.Checked || ChkNYHA4_2.Checked;

        bool blnIVAA = ChkIVAA.Checked;
        bool blnurgcv = Chkurgcv.Checked;
        bool blndefib = Chkdefib.Checked;

        bool blnAAcon = false;
        bool blnAAbolus = false;
        bool blnAAtitr = false;
        int intAAmonitor = 0;
        bool blnPOAA = false;

        if (blnIVAA)
        {
            blnAAcon = ChkAAcon.Checked;
            blnAAbolus = ChkAAbolus.Checked;
            blnAAtitr = ChkAAtitr.Checked;
            intAAmonitor = Val_int(TxtAAmonitor.Text);
            blnPOAA = ChkPOAA.Checked;
        }
        else
        {
            blnAAcon = false;
            blnAAbolus = false;
            blnAAtitr = false;
            intAAmonitor = 0;
            blnPOAA = false;
        }

        bool blnIVHT = ChkIVHT.Checked;
        bool blnIVVA = ChkIVVA.Checked;
        bool blnIVDI = ChkIVDI.Checked;


        bool blnHTcon = false;
        bool blnHTbolus = false;
        bool blnHTtitr = false;
        int intHTmonitor = 0;

        if (blnIVHT)
        {
            blnHTcon = ChkHTcon.Checked;
            blnHTbolus = ChkHTbolus.Checked;
            blnHTtitr = ChkHTtitr.Checked;
            intHTmonitor = Val_int(TxtHTmonitor.Text);
        }
        else
        {
            blnHTcon = false;
            blnHTbolus = false;
            blnHTtitr = false;
            intHTmonitor = 0;
        }

        bool blnVAcon = false;
        bool blnVAbolus = false;
        bool blnVAtitr = false;
        bool blnIVNPS = false;
        bool blnIVNTG = false;
        bool blnmilrinone = false;
        int intVAmonitor = 0;

        if (blnIVVA)
        {
            blnVAcon = ChkVAcon.Checked;
            blnVAbolus = ChkVAbolus.Checked;
            blnVAtitr = ChkVAtitr.Checked;
            intVAmonitor = Val_int(TxtVAmonitor.Text);
            blnIVNPS = ChkIVNPS.Checked;
            blnIVNTG = ChkIVNTG.Checked;
            blnmilrinone = Chkmilrinone.Checked;
        }
        else
        {
            blnVAcon = false;
            blnVAbolus = false;
            blnVAtitr = false;
            intVAmonitor = 0;
            blnIVNPS = false;
            blnIVNTG = false;
            blnmilrinone = false;
        }

        bool blncon = false;
        int intmonitor = 0;
        bool blnPODI = false;
        bool blncar = Chkcar.Checked;
        bool blndiuretic = Chkdiuretic.Checked;
        bool blnace = Chkace.Checked;
        bool blnCCB = ChkCCB.Checked;
        bool blnvasodilator = Chkvasodilator.Checked;
        bool blnCarvedilol625 = false;
        bool blnCarvedilol125 = false;
        bool blnCarvedilol25 = false;
        bool blnMetoprololER50 = false;
        bool blnMetoprololER100 = false;
        bool blnMetoprololER200 = false;
        bool blnBisoprolol25 = false;
        bool blnBisoprolol5 = false;
        bool blnBisoprolol10 = false;
        bool blnfurosemide40 = false;
        bool blnfurosemide80 = false;
        bool blnfurosemide81 = false;
        bool blnbumex1 = false;
        bool blnbumex2 = false;
        bool blnbumex3 = false;
        bool blntorsemide20 = false;
        bool blntorsemide40 = false;
        bool blntorsemide50 = false;
        bool blnHCTZ = false;
        bool blnindapamide = false;
        bool blnchlorthalidone = false;
        bool blnlisinopril5 = false;
        bool blnlisinopril10 = false;
        bool blnlisinopril20 = false;
        bool blnlosartan25 = false;
        bool blnlosartan50 = false;
        bool blnlosartan100 = false;

        if (blnIVDI)
        {
            blncon = Chkcon.Checked;
            intmonitor = Val_int(Txtmonitor.Text);
            blnPODI = ChkPODI.Checked;
        }
        else
        {
            blncon = false;
            intmonitor = 0;
            blnPODI = false;
        }



        bool blnNIPPV = ChkNIPPV.Checked;
        int intosupp = Val_int(txtOsupp.Text);

        int intRI = 0;

        if (blnNIPPV)
        {
            intRI = Val_int(txtRI.Text);
        }
        else
        {
            intRI = 0;
        }
        if (blncar)
        {
            blnCarvedilol625 = chkCarvedilol625.Checked;
            blnCarvedilol125 = chkCarvedilol125.Checked;
            blnCarvedilol25 = chkCarvedilol25.Checked;
            blnMetoprololER50 = chkMetoprololER50.Checked;
            blnMetoprololER100 = chkMetoprololER100.Checked;
            blnMetoprololER200 = chkMetoprololER200.Checked;
            blnBisoprolol25 = chkBisoprolol25.Checked;
            blnBisoprolol5 = chkBisoprolol5.Checked;
            blnBisoprolol10 = chkBisoprolol10.Checked;
        }
        else
        {
            blnCarvedilol625 = false;
            blnCarvedilol125 = false;
            blnCarvedilol25 = false;
            blnMetoprololER50 = false;
            blnMetoprololER100 = false;
            blnMetoprololER200 = false;
            blnBisoprolol25 = false;
            blnBisoprolol5 = false;
            blnBisoprolol10 = false;
        }
        if (blndiuretic)
        {
            blnfurosemide40 = chkfurosemide40.Checked;
            blnfurosemide80 = chkfurosemide80.Checked;
            blnfurosemide81 = chkfurosemide81.Checked;
            blnbumex1 = chkbumex1.Checked;
            blnbumex2 = chkbumex2.Checked;
            blnbumex3 = chkbumex3.Checked;
            blntorsemide20 = chktorsemide20.Checked;
            blntorsemide40 = chktorsemide40.Checked;
            blntorsemide50 = chktorsemide50.Checked;
            blnHCTZ = ChkHCTZ.Checked;
            blnindapamide = Chkindapamide.Checked;
            blnchlorthalidone = Chkchlorthalidone.Checked;

        }
        else
        {
            blnfurosemide40 = false;
            blnfurosemide80 = false;
            blnfurosemide81 = false;
            blnbumex1 = false;
            blnbumex2 = false;
            blnbumex3 = false;
            blntorsemide20 = false;
            blntorsemide40 = false;
            blntorsemide50 = false;
            blnHCTZ = false;
            blnindapamide = false;
            blnchlorthalidone = false;
        }
        if (blnace)
        {
            blnlisinopril5 = chklisinopril5.Checked;
            blnlisinopril10 = chklisinopril10.Checked;
            blnlisinopril20 = chklisinopril20.Checked;
            blnlosartan25 = chklosartan25.Checked;
            blnlosartan50 = chklosartan50.Checked;
            blnlosartan100 = chklosartan100.Checked;
        }
        else
        {
            blnlisinopril5 = false;
            blnlisinopril10 = false;
            blnlisinopril20 = false;
            blnlosartan25 = false;
            blnlosartan50 = false;
            blnlosartan100 = false;

        }
        bool blnult = chkult.Checked;
        bool blnIABP = ChkIABP.Checked;
        bool blnVAD = ChkVAD.Checked;
        bool blnTPM = ChkTPM.Checked;




        bool blnclass1 = false;
        bool blnclass2 = false;
        bool blnclass3 = false;

        if (blnDOE)
        {
            blnclass1 = chkclass1.Checked;
            blnclass2 = chkclass2.Checked;
            blnclass3 = chkclass3.Checked;
        }
        else
        {
            blnclass1 = false;
            blnclass2 = false;
            blnclass3 = false;
        }


        bool blnTestCHD = false;
        bool blnTestACS = chkACS.Checked;
        bool blnPCI = chkPCI.Checked;
        bool blnStenusis = chkSTENUSIS.Checked;
        bool blnLAD = chkLAD.Checked;
        bool blnCTO = chkCTO.Checked;
        bool blnLM = chkLM.Checked;
        int intNumVessel = Val_int(txtNumVessel.Text);
        double dblSTEMIdur = Val_dbl(TxtSTEMIdur.Text);

        bool blnRCA = ChkRCA.Checked;
        bool blncoraneurysm = Chkcoraneurysm.Checked;
        bool blncordissection = Chkcordissection.Checked;
        bool blncalcified = Chkcalcified.Checked;
        bool blnlipidrich = Chklipidrich.Checked;
        bool blnLCX = ChkLCX.Checked;
        bool blnvasospasm = Chkvasospasm.Checked;

        if (blnTestischemic || blnTestMI || blnTestACS || blnPCI || blnStenusis || blnLAD || blnCTO || blnLM || !string.IsNullOrEmpty(txtNumVessel.Text.Trim()) || blnRCA || blncoraneurysm || blncordissection || blncalcified || blnlipidrich || blnLCX)
        {
            blnTestCHD = true;
        }

        bool blnang24 = false;
        bool blnan20 = false;
        bool blnASA = false;
        bool blntrop = false;
        bool blnAnAn = false;
        bool blnchangetrop = false;
        bool blnrefractory = false;
        bool blnSTEMI = false;
        int intLVEF = 0;
        if (!string.IsNullOrEmpty(TxtHF.Text.Trim()))
        {
            intLVEF = Val_int(TxtHF.Text);
            intTestCHF = intLVEF;
        }
        else
        {
            intLVEF = intTestCHF;
        }
        if (intLVEF < 40 & intLVEF > 0)
        {
            blnTestHF = true;
        }


        if (blnTestACS)
        {
            blnang24 = Chkang24.Checked;
            blnan20 = ChkAn20.Checked;
            blnASA = ChkASA.Checked;
            blntrop = Chktrop.Checked;
            blnAnAn = ChkAnAn.Checked;
            blnchangetrop = chkchangetrop.Checked;
            blnrefractory = chkrefractory.Checked;
            blnSTEMI = chkSTEMI.Checked;
        }
        else
        {
            blnang24 = false;
            blnan20 = false;
            blnASA = false;
            blntrop = false;
            blnAnAn = false;
            blnchangetrop = false;
            blnrefractory = false;
            blnSTEMI = false;
        }

        bool blnanterior = false;
        bool blninferior = false;
        bool blnother = false;

        if (blnSTEMI)
        {
            blnanterior = chkanterior.Checked;
            blninferior = chkinferior.Checked;
            blnother = chkother.Checked;
        }
        else
        {
            blnanterior = false;
            blninferior = false;
            blnother = false;
        }

        bool blnBMS = false;
        bool blnDES = false;
        bool blnCABG = false;

        if (blnPCI)
        {
            blnBMS = ChkBMS.Checked;
            blnDES = ChkDES.Checked;
            blnCABG = ChkCABG.Checked;
        }
        else
        {
            blnBMS = false;
            blnDES = false;
            blnCABG = false;
        }

        bool blnautovein = false;
        bool blnautoartery = false;
        bool blnnonauto = false;

        if (blnCABG)
        {
            blnautovein = chkautovein.Checked;
            blnautoartery = chkautoartery.Checked;
            blnnonauto = chknonauto.Checked;
        }
        else
        {
            blnautovein = false;
            blnautoartery = false;
            blnnonauto = false;
        }

        int intprocedure = Val_int(Txtprocedure.Text);

        bool blnAF = false;
        bool blnparoxysmal = Chkparoxysmal.Checked;
        bool blnpersistent = Chkpersistent.Checked;
        bool blnchronicAF = ChkchronicAF.Checked;
        int intCMP = Val_int(TxtCMP.Text);
        int intafd = Val_int(Txtafd.Text);
        bool blnCAA = chkCAA.Checked;
        bool blnTestPrevious = chkPrevious.Checked;
        int intPrevious = Val_int(TxtPrevious.Text);
        bool blnTestThyro = chkThyro.Checked;
        bool blnTestHypoThyro = chkHypoThyro.Checked;
        bool blnacutely = Chkacutely.Checked;
        bool blnCAC = ChkCAC.Checked;
        bool blnFactorX = chkFactorX.Checked;
        bool blnliver = Chkliver.Checked;
        bool blnablation = Chkablation.Checked;
        bool blnVHD = ChkVHD.Checked;
        bool blnpharm = Chkpharm.Checked;
        bool blnCV = ChkCV.Checked;
        bool blnclass1AA = false;
        bool blnclass3AA = false;

        if (blnVHD)
        {
            blnacutely = Chkacutely.Checked;
        }
        else
        {
            blnacutely = false;

        }
        if (blnCAA)
        {
            blnclass1AA = chkclass1AA.Checked;
            blnclass3AA = chkclass3AA.Checked;
        }
        else
        {
            blnclass1AA = false;
            blnclass3AA = false;
        }



        if (blnparoxysmal || blnpersistent || blnchronicAF  || blnCAA  || blnCAC || blnVHD || blnpharm || blnCV)
        {
            blnAF = true;
        }

        bool blnThromboembolic = false;
        bool blnVKA = ChkVKA.Checked;
        double dblINR = Val_dbl(txtINR.Text);
        bool blnacutecva = chkacutecva.Checked;

        int intNIHSS = 0;
        bool blnpreh = false;

        if (blnacutecva)
        {
            intNIHSS = Val_int(txtNIHSS.Text);
            blnpreh = chkpreh.Checked;
            //CheckBox1
            //CheckBox2
            //ssdur
        }
        else
        {
            intNIHSS = 0;
            blnpreh = false;
            //CheckBox1
            //CheckBox2
            //ssdur
        }

        bool blnDeepVenousThrombosis = chkDeepVenousThrombosis.Checked;
        bool blnDeepVenous = chkDeepVenous.Checked;
        bool blnPE = chkPE.Checked;
        bool blnprior = chkprior.Checked;
        bool blnpriorASA = chkpriorASA.Checked;
        bool blnDVTprevention = chkDVTprevention.Checked;

        bool blnbldrisk = false;
        bool blngeneral = false;
        bool blnurology = false;
        bool blnknee = false;
        bool blnhip = false;
        bool blnHPS = false;
        bool blncancer = false;
        bool blnspinal = false;
        bool blnacute = false;

        bool blnsaddle = false;
        bool blnseptic = false;
        bool blnotherPE = false;

        if (blnDVTprevention)
        {
            blnbldrisk = chkbldrisk.Checked;
            blngeneral = Chkgeneral.Checked;
            blnurology = Chkurology.Checked;
            blnknee = Chkknee.Checked;
            blnhip = Chkhip.Checked;
            blnHPS = ChkHPS.Checked;
            blncancer = Chkcancer.Checked;
            blnspinal = Chkspinal.Checked;
            blnacute = Chkacute.Checked;
        }
        else
        {
            blnbldrisk = false;
            blngeneral = false;
            blnurology = false;
            blnknee = false;
            blnhip = false;
            blnHPS = false;
            blncancer = false;
            blnspinal = false;
            blnacute = false;
        }
        if (blnPE)
        {
            blnsaddle = chksaddle.Checked;
            blnseptic = chkseptic.Checked;
            blnotherPE = chkotherPE.Checked;
        }
        else
        {
            blnsaddle = false;
            blnseptic = false;
            blnotherPE = false;
        }

        bool blnbleed = Chkbleed.Checked;
        bool blnCVC = false;
        bool blnICU = false;
        bool blnplt = false;
        bool blnPUD = ChkPUD.Checked;
        bool blnDVTE = ChkDVTE.Checked;
        bool blnrheum = Chkrheum.Checked;
        bool blnTIA = ChkTIA.Checked;

        if (blnbldrisk)
        {
            blnplt = Chkplt.Checked;
            blnCVC = ChkCVC.Checked;
            blnICU = ChkICU.Checked;
        }
        else
        {
            blnplt = false;
            blnCVC = false;
            blnICU = false;
        }

        bool blnactivecancer = false;
        bool blnPVTE = ChkPVTE.Checked;
        bool blnRF = false;
        bool blnthrombo = false;
        bool blnhormone = false;
        bool blninfection = false;
        bool blnrecent = false;
        bool blnreduced = false;
        bool blnhemoptysis = Chkhemoptysis.Checked;
        bool blnunilateral = Chkunilateral.Checked;
        bool blnulcer = chkulcer.Checked;
        bool blnswelling = false;

        if (blnacute)
        {
            blnactivecancer = Chkactivecancer.Checked;
            blnRF = ChkRF.Checked;
            blnhormone = Chkhormone.Checked;
            blninfection = Chkinfection.Checked;
            blnrecent = ChkRecent.Checked;
            blnreduced = ChkReduced.Checked;
            blnthrombo = Chkthrombo.Checked;
            blnswelling = Chkswelling.Checked;
        }
        else
        {
            blnactivecancer = false;
            blnhormone = false;
            blninfection = false;
            blnrecent = false;
            blnreduced = false;
            blnRF = false;
            blnthrombo = false;
            blnswelling = false;
        }

        bool blncage = Chkcage.Checked;
        bool blnmech = chkmech.Checked;
        bool blnmdav = chkMdav.Checked;
        bool blnRMVD = chkRMVD.Checked;
        bool blnbiop = chkbiop.Checked;
        bool blnsysemb = Chksysemb.Checked;

        bool blnLAthr = false;
        int intLAd = 0;

        if (blnRMVD)
        {
            blnLAthr = ChkLAthr.Checked;
            intLAd = Val_int(txtLAd.Text);
        }
        else
        {
            blnLAthr = false;
            intLAd = 0;
        }

        bool blnIE = chkIE.Checked;
        bool blnLPVT = chkLPVT.Checked;
        bool blnRPVT = chkRPVT.Checked;
        bool blnArterialIschemic = chkArterialIschemic.Checked;
        bool blnAorticArchAth = chkAorticArchAth.Checked;
        bool blnTestLVt = ChkLVt.Checked;
        bool blnPFO = chkPFO.Checked;
        bool blnMVP = chkMVP.Checked;
        bool blnMitralAnnular = chkMitralAnnular.Checked;
        bool blnCalcificAortic = chkCalcificAortic.Checked;
        bool blnHeparinIndThrom = chkHeparinIndThrom.Checked;
        bool blnApoA1 = ChkApoA1.Checked;
        bool blnCerebralVenous = chkCerebralVenous.Checked;
        bool blnminorpro = chkminorpro.Checked;
        bool blnsurgery = chksurgery.Checked;
        bool blnemergencysurgery = chkemergencysurgery.Checked;
        if (blnVKA || !string.IsNullOrEmpty(txtINR.Text.Trim()) || blnacutecva || blnDeepVenousThrombosis || blnDeepVenous || blnPE || blnDVTprevention || blncage || blnmech || blnmdav || blnRMVD || blnbiop || blnsysemb || blnIE || blnLPVT || blnRPVT || blnArterialIschemic || blnAorticArchAth || blnTestLVt || blnPFO || blnMVP || blnMitralAnnular || blnCalcificAortic || blnHeparinIndThrom || blnApoA1 || blnCerebralVenous)
        {
            blnThromboembolic = true;
        }

        bool blnTestSYNCOPE = ChkTestSyncope.Checked;
        bool blnSinusNodeDysfunction = ChkSinusNodeDysfunction.Checked;
        bool blnAVNodeDisease = ChkAVNodeDisease.Checked;
        bool blnVasoDepressorSyncope = ChkVasoDepressorSyncope.Checked;
        bool blnCarotidSinusHypersensitivity = ChkCarotidSinusHypersensitivity.Checked;
        bool blnPosturalOrthostatisTac = ChkPosturalOrthostatisTac.Checked;
        bool blnAutonomicFailureSyndrome = ChkAutonomicFailureSyndrome.Checked;
        bool blnHistCardArrest = ChkHistCardArrest.Checked;
        bool blnBrugada = ChkBrugada.Checked;
        bool blnLongQT = ChkLongQT.Checked;
        bool blnsarc = Chksarc.Checked;

        bool blnSynStanding = false;
        bool blnSynsporadic = false;

        bool blnSynProSitting = false;
        bool blnSynOther = false;
        bool blnSynneck = false;
        bool blnSynNausea = false;
        bool blnSynDizziness = false;
        bool blnSynPalpitations = false;
        bool blnSynProCP = false;
        int intSynMin = 0;
        int intSynSec = 0;
        bool blnSynNone = false;
        bool blnSynDisor = false;
        bool blnSynPara = false;
        bool blnSynPosCP = false;
        bool blnSynaura = false;
        bool blnSynpallor = false;
        bool blnSynposture = false;
        bool blnSynfatigue = false;
        bool blnSyntrauma = false;


        if (blnTestSYNCOPE)
        {
            blnSynStanding = chkSynStanding.Checked;
            blnSynsporadic = chkSynsporadic.Checked;
            blnSynProSitting = chkSynProSitting.Checked;
            blnSynOther = chkSynOther.Checked;
            blnSynneck = chkSynneck.Checked;
            blnSynNausea = chkSynNausea.Checked;
            blnSynDizziness = chkSynDizziness.Checked;
            blnSynPalpitations = chkSynPalpitations.Checked;
            blnSynProCP = chkSynProCP.Checked;
            intSynMin = Val_int(txtSynMin.Text);
            intSynSec = Val_int(txtSynSec.Text);
            blnSynNone = chkSynNone.Checked;
            blnSynDisor = chkSynDisor.Checked;
            blnSynPara = chkSynPara.Checked;
            blnSynPosCP = chkSynPosCP.Checked;
            blnSynaura = chkSynaura.Checked;
            blnSynpallor = chkSynpallor.Checked;
            blnSynposture = chkSynposture.Checked;
            blnSynfatigue = chkSynfatigue.Checked;
            blnSyntrauma = chkSyntrauma.Checked;
        }
        else
        {
            blnSynStanding = false;
            blnSynsporadic = false;
            blnSynProSitting = false;
            blnSynOther = false;
            blnSynneck = false;
            blnSynNausea = false;
            blnSynDizziness = false;
            blnSynPalpitations = false;
            blnSynProCP = false;

            intSynMin = 0;
            intSynSec = 0;
            blnSynNone = false;
            blnSynDisor = false;
            blnSynPara = false;
            blnSynPosCP = false;
            blnSynaura = false;
            blnSynpallor = false;
            blnSynposture = false;
            blnSynfatigue = false;
            blnSyntrauma = false;
        }


        bool blnTestEd = false;
        bool blnTestNSVT = ChkNSVT.Checked;
        bool blnMVT = chkMVT.Checked;
        bool blnRMVT = chkRMVT.Checked;
        bool blnPVT = chkPVT.Checked;
        bool blntors = chkTors.Checked;
        bool blnIVT = chkIVT.Checked;
        bool blnidioVT = chkidioVT.Checked;

        if (blnTestNSVT || blnMVT || blnRMVT || blnPVT || blntors || blnIVT || blnidioVT)
        {
            blnTestEd = true;
        }

        bool blnTA = chkTA.Checked;
        bool blnPA = chkPA.Checked;
        bool blnescTA = false;

        if (blnTA || blnPA)
        {
            blnescTA = chkescTA.Checked;
        }
        else
        {
            blnescTA = false;
        }

        bool blnNCP = chkNCP.Checked;
        bool blnunable = chkunable.Checked;
        bool blnER = chkER.Checked;
        bool blnangina = false;
        bool blnangina20 = false;
        bool blnpositivetrop = chkpositivetrop.Checked;
        bool blntrop2 = false;
        bool blntrop3 = false;
        if (blnpositivetrop)
        {
            blntrop2 = Chktrop2.Checked;
            blntrop3 = Chktrop3.Checked;
        }
        else
        {
            blntrop2 = false;
            blntrop3 = false;
        }



        bool blnASAuse = false;

        bool blnpleur = false;
        bool blnCPbreath = false;
        bool blnPrecordial = false;

        if (blnNCP)
        {
            blnpleur = chkpleur.Checked;
            blnCPbreath = chkCPbreath.Checked;
            blnPrecordial = chkPrecordial.Checked;
        }
        else
        {
            blnpleur = false;
            blnCPbreath = false;
            blnPrecordial = false;
        }

        bool blnCPSharp = false;
        bool blnCPPressure = false;
        bool blnCPTightness = false;
        bool blnCPSoreness = false;
        bool blnTearing = false;
        bool blnCPLying = false;
        bool blnCPBreathing = false;
        bool blnCPCoughing = false;
        bool blnCPExertion = false;
        bool blnCPPalpation = false;
        bool blnCPNTG = false;
        bool blnCPRest = false;
        bool blnCPSitting = false;
        bool blnCPlyingside = false;
        bool blnCPAnalgesics = false;
        bool blnCPJaw = false;
        bool blnCPBack = false;
        bool blnCPArm = false;
        bool blnCPDyspnea = false;
        bool blnCPNausea = false;
        bool blnCPDiaph = false;
        bool blnCPFaint = false;
        bool blnCPpalpitations = false;
        bool blnCPseconds = false;
        bool blnCPminutes = false;
        bool blnCPhours = false;
        bool blnCPconstant = false;
        bool blnrub = false;
        bool blnbreath = false;
        bool blnabdominal = false;
        int intDiffInSBP = 0;

        if (blnpleur | blnCPbreath | blnPrecordial)
        {
            blnCPSharp = chkCPSharp.Checked;
            blnCPPressure = chkCPPressure.Checked;
            blnCPTightness = chkCPTightness.Checked;
            blnCPSoreness = chkCPSoreness.Checked;
            blnTearing = Chktearing.Checked;
            blnCPLying = chkCPLying.Checked;
            blnCPBreathing = chkCPBreathing.Checked;
            blnCPCoughing = chkCPCoughing.Checked;
            blnCPExertion = chkCPExertion.Checked;
            blnCPPalpation = chkCPPalpation.Checked;
            blnCPNTG = chkCPNTG.Checked;
            blnCPRest = chkCPRest.Checked;
            blnCPSitting = chkCPSitting.Checked;
            blnCPlyingside = chkCPlyingside.Checked;
            blnCPAnalgesics = chkCPAnalgesics.Checked;
            blnCPJaw = chkCPJaw.Checked;
            blnCPBack = chkCPBack.Checked;
            blnCPArm = chkCPArm.Checked;
            blnCPDyspnea = chkCPDyspnea.Checked;
            blnCPNausea = chkCPNausea.Checked;
            blnCPDiaph = chkCPDiaph.Checked;
            blnCPFaint = chkCPFaint.Checked;
            blnCPpalpitations = chkCPpalpitations.Checked;
            blnCPseconds = chkCPseconds.Checked;
            blnCPminutes = chkCPminutes.Checked;
            blnCPhours = chkCPhours.Checked;
            blnCPconstant = chkCPconstant.Checked;
            blnrub = chkrub.Checked;
            blnbreath = chkbreath.Checked;
            blnabdominal = chkabdominal.Checked;
            intDiffInSBP = Val_int(txtDiffInSBP.Text);
        }
        else
        {
            blnCPSharp = false;
            blnCPPressure = false;
            blnCPTightness = false;
            blnCPSoreness = false;
            blnTearing = false;
            blnCPLying = false;
            blnCPBreathing = false;
            blnCPCoughing = false;
            blnCPExertion = false;
            blnCPPalpation = false;
            blnCPNTG = false;
            blnCPRest = false;
            blnCPSitting = false;
            blnCPlyingside = false;
            blnCPAnalgesics = false;
            blnCPJaw = false;
            blnCPBack = false;
            blnCPArm = false;
            blnCPDyspnea = false;
            blnCPNausea = false;
            blnCPDiaph = false;
            blnCPFaint = false;
            blnCPpalpitations = false;
            blnCPseconds = false;
            blnCPminutes = false;
            blnCPhours = false;
            blnCPconstant = false;
            blnrub = false;
            blnbreath = false;
            blnabdominal = false;
            intDiffInSBP = 0;
        }

        if (blnER)
        {
            blnangina = chkangina.Checked;
            blnangina20 = chkangina20.Checked;
            blnpositivetrop = chkpositivetrop.Checked;
            blnASAuse = chkASAuse.Checked;
        }
        else
        {
            blnangina = false;
            blnangina20 = false;
            blnpositivetrop = false;
            blnASAuse = false;
        }

        bool blnTestSCD = false;
        bool blnNoncardioCVA = chkNoncardioCVA.Checked;
        bool blnNoncardioTIA = chkNoncardioTIA.Checked;
        bool blnCEA = chkCEA.Checked;
        if (blnNoncardioCVA || blnCEA || blnNoncardioTIA)
        {
            blnTestSCD = true;
        }

        bool blnTestPVD = false;
        bool blnABI = chkABI.Checked;
        bool blnclaudication = Chkclaudication.Checked;
        bool blncritical = Chkcritical.Checked;
        bool blnbypass = Chkbypass.Checked;
        bool blnBKbypass = ChkBKbypass.Checked;
        if (blnABI || blnclaudication || blncritical || blnbypass || blnBKbypass)
        {
            blnTestPVD = true;
        }

        bool blnTestAAA = false;
        bool blnAADiameter = chkAADiameter.Checked;
        bool blnthoracic = chkthoracic.Checked;
        bool blnthoracoabdominal = chkthoracoabdominal.Checked;

        if (blnAADiameter || blnthoracic || blnthoracoabdominal)
        {
            blnTestAAA = true;
        }

        bool blnTestDM = chkDM.Checked;
        bool blnTestDM1 = chkDM1.Checked;
        double dblTestMicro = 0;
        if ((string.IsNullOrEmpty(txtMICRO.Text.Trim())))
        {
            if (chkDM.Checked == true || chkDM1.Checked == true)
            {
                dblTestMicro = 30.000009;
                
            }
            
        }
        else
        {
           
            
                dblTestMicro = Val_int(txtMICRO.Text);
            
        }
        bool blngestational = chkgestational.Checked;
        bool blnDMNP = false;
        bool blnDMCKD = false;
        bool blnDMother = false;
        bool blnDMmono = false;
        bool blnDMpoly = false;
        bool blnDMautonom = false;
        bool blnDMangio = false;
        bool blnDMgangrene = false;
        bool blnDMothercirc = false;
        bool blnDMarthro = false;
        bool blnDMskin = false;
        bool blnDMoral = false;
        bool blnDMhypo = false;
        
        bool blnDMhypocoma = false;
        bool blnDMhyper = false;
       
        bool blnDMothercomp = false;
        bool blnDMunspec = false;
        bool blnDMwithout = false;
       

        if (chkDM.Checked||chkDM1.Checked)
        {
            blnDMNP = chkDMNP.Checked;
            blnDMCKD = chkDMCKD.Checked;
            blnDMother = chkDMother.Checked;
            blnDMmono = chkDMmono.Checked;
            blnDMpoly = chkDMpoly.Checked;
            blnDMautonom = chkDMautonom.Checked;
            blnDMangio = chkDMangio.Checked;
            blnDMgangrene = chkDMgangrene.Checked;
            blnDMothercirc = chkDMothercirc.Checked;
            blnDMarthro = chkDMarthro.Checked;
            blnDMskin = chkDMskin.Checked;
            blnDMoral = chkDMoral.Checked;
            blnDMhypo = chkDMhypo.Checked;
            blnDMhypocoma = chkDMhypocoma.Checked;
            blnDMhyper = chkDMhyper.Checked;
            blnDMothercomp = chkDMothercomp.Checked;
            blnDMunspec = chkDMunspec.Checked;
            blnDMwithout =  chkDMwithout.Checked;
        }
        else
        {
            blnDMNP = false;
            blnDMCKD = false;
            blnDMother = false;
            blnDMmono = false;
            blnDMpoly = false;
            blnDMautonom = false;
            blnDMangio = false;
            blnDMgangrene = false;
            blnDMothercirc = false;
            blnDMarthro = false;
            blnDMskin = false;
            blnDMoral = false;
            blnDMhypo = false;
            blnDMhypocoma = false;
            blnDMhyper = false;
            blnDMothercomp = false;
            blnDMunspec = false;
            blnDMwithout = false;
        }
        bool blnRetinopathy = ChkRetinopathy.Checked;

        bool blnTestSAH = chkSAH.Checked;
        bool blnTestSmoke = chkSmoke.Checked;
        bool blnTestCurrently = blnTestSmoke;
        bool blnTestFamily = chkFamily.Checked;
        bool blnsediment = chksediment.Checked;
        bool blnrenalimage = false;
        bool blnhistology = false;
        bool blnCKD = chkCKD.Checked;
        if (chkCKD.Checked)
        {
           
            blnrenalimage = chkrenalimage.Checked;
            blnhistology = chkhistology.Checked;

        }
        else
        {
           
            blnrenalimage = false;
            blnhistology = false;

        }
        bool blnRBC = false;
        bool blnRBCcast = false;
        bool blnWBCcast = false;
        bool blngranular = false;
        bool blnoval = false;
        if (chksediment.Checked)
        {
            blnRBC = chkRBC.Checked;
            blnRBCcast = chkRBCcast.Checked;
            blnWBCcast = chkWBCcast.Checked;
            blngranular = chkgranular.Checked;
            blnoval = chkoval.Checked;

        }
        else
        {
            blnRBC = false;
            blnRBCcast = false;
            blnWBCcast = false;
            blngranular = false;
            blnoval = false;

        }
        bool blnTestSBPTreated = false;
        bool blnAA = false;
        bool blnaldo = false;
        bool blnrenovas = false;
        bool blnFMD = false;

        
        bool blnhead = false;
        bool blnepis = false;
        bool blnCP = false;

        if (chkSAH.Checked)
        {
            blnTestSBPTreated = chkSBPTreated.Checked;
            blnAA = chkAA.Checked;
            blnaldo = Chkaldo.Checked;
            blnrenovas = Chkrenovas.Checked;
            blnFMD = ChkFMD.Checked;
            
            blnhead = Chkhead.Checked;
            blnepis = Chkepis.Checked;
            blnCP = ChkCP.Checked;
        }
        else
        {
            blnTestSBPTreated = false;
            blnAA = false;
            blnaldo = false;
            blnrenovas = false;
            blnFMD = false;
          
            blnhead = false;
            blnepis = false;
            blnCP = false;
        }

        bool blnelective = Chkelective.Checked;
        bool blnemergency = Chkemergency.Checked;
        bool blnunab = Chkunab.Checked;



        bool blnvascular = Chkvascular.Checked;
        bool blnlowriskelec = Chklowriskelec.Checked;


        bool blnasthma = chkasthma.Checked;
        int intasymptoms = 0;
        int intnocturnal= 0;
        int intSABA = 0;
        bool blninterference = false;

        if (chkasthma.Checked)
        {
            intasymptoms = Val_int(txtasymptoms.Text);
            intnocturnal = Val_int(txtnocturnal.Text);
            intSABA = Val_int(txtSABA.Text);
           
            blninterference = chkinterference.Checked;
            
        }
        else
        {
            intasymptoms = 0;
            intnocturnal = 0;
            intSABA = 0;
            blninterference= false;
           
        }
        bool blnnone = false;
        bool blnminor = false;
        bool blnsome = false;
        bool blnsignificant = false;
        if (chkinterference.Checked)
        {
            
            blnnone = chknone.Checked;
            blnminor = chkminor.Checked;
            blnsome = chksome.Checked;
            blnsignificant = chksignificant.Checked;
        }
        else
        {
           
            blnnone = false;
            blnminor = false;
            blnsome = false;
            blnsignificant = false;
        }
        bool blnILD = chkILD.Checked;
        bool blnCOPD = chkCOPD.Checked;

        string strTestTargetHdl = "";
        //= lblTargetHdl.Text
        string strTestTargetLdl = "";
        //= lblTargetLdl.Text
        string strTargetSBP = "";
        string strTargetDBP = "";
        int intTestStatin = 0;
        //= Val(lblStatin.Text)
        bool blnSVT = false;


        bool blnHeartMurmur = chkHeartMurmur.Checked;
        bool blnS1Mitral = false;
        bool blnS2Aortic = false;
        bool blnP2Pulmonic = false;
        bool blnS1Tricuspid = false;

        bool blnSystolicMurmur = false;
        bool blnDiastolicMurmur = false;
        bool blnMitralOpenSnap = false;
        bool blnsystolicclick = false;
        bool blnejectionsound = false;

        if (blnHeartMurmur)
        {
            blnS1Mitral = chkS1Mitral.Checked;
            blnS2Aortic = chkS2Aortic.Checked;
            blnP2Pulmonic = chkP2Pulmonic.Checked;
            blnS1Tricuspid = chkS1Tricuspid.Checked;
            blnSystolicMurmur = chkSystolicMurmur.Checked;
            blnDiastolicMurmur = chkDiastolicMurmur.Checked;
        }
        else
        {
            blnS1Mitral = false;
            blnS2Aortic = false;
            blnP2Pulmonic = false;
            blnS1Tricuspid = false;
            blnSystolicMurmur = false;
            blnDiastolicMurmur = false;
        }

        bool blnS1MitralLoud = false;
        bool blnS1MitralNormal = false;
        bool blnS1MitralSoft = false;

        if (blnS1Mitral)
        {
            blnS1MitralLoud = chkS1MitralLoud.Checked;
            blnS1MitralNormal = chkS1MitralNormal.Checked;
            blnS1MitralSoft = chkS1MitralSoft.Checked;
        }
        else
        {
            blnS1MitralLoud = false;
            blnS1MitralNormal = false;
            blnS1MitralSoft = false;
        }
        bool blnS1TricuspidLoud = false;
        bool blnS1TricuspidNormal = false;
        bool blnS1TricuspidSoft = false;

        if (blnS1Mitral)
        {
            blnS1TricuspidLoud = chkS1TricuspidLoud.Checked;
            blnS1TricuspidNormal = chkS1TricuspidNormal.Checked;
            blnS1TricuspidSoft = chkS1TricuspidSoft.Checked;
        }
        else
        {
            blnS1TricuspidLoud = false;
            blnS1TricuspidNormal = false;
            blnS1TricuspidSoft = false;
        }

        bool blnS2AorticLoud = false;
        bool blnS2AorticNormal = false;
        bool blnS2AorticSoft = false;

        if (blnS2Aortic)
        {
            blnS2AorticLoud = chkS2MitralLoud.Checked;
            blnS2AorticNormal = chkS2MitralNormal.Checked;
            blnS2AorticSoft = chkS2MitralSoft.Checked;
        }
        else
        {
            blnS2AorticLoud = false;
            blnS2AorticNormal = false;
            blnS2AorticSoft = false;
        }

        bool blnP2PulmonicLoud = false;
        bool blnP2PulmonicNormal = false;
        bool blnP2PulmonicSoft = false;

        if (blnP2Pulmonic)
        {
            blnP2PulmonicLoud = chkP2PulmonicLoud.Checked;
            blnP2PulmonicNormal = chkP2PulmonicNormal.Checked;
            blnP2PulmonicSoft = chkP2PulmonicSoft.Checked;
        }
        else
        {
            blnP2PulmonicLoud = false;
            blnP2PulmonicNormal = false;
            blnP2PulmonicSoft = false;
        }

        bool blnCresDecres = false;
        bool blnPlateuShaped = false;
        bool blnsquat = false;

        if (blnSystolicMurmur)
        {
            blnCresDecres = chkCresDecres.Checked;
            blnPlateuShaped = chkPlateuShaped.Checked;
            blnsquat = chksquat.Checked;
            blnsystolicclick = chksystolicclick.Checked;
            blnejectionsound = chkejectionsound.Checked;
        }
        else
        {
            blnCresDecres = false;
            blnPlateuShaped = false;
            blnsquat = false;
            blnsystolicclick = false;
            blnejectionsound = false;
        }

        bool blnEarlyMidSysPeak = false;
        bool blnLateSysPeak = false;

        if (blnCresDecres)
        {
            blnEarlyMidSysPeak = chkEarlyMidPeak.Checked;
            blnLateSysPeak = chkLatePeak.Checked;
        }
        else
        {
            blnEarlyMidSysPeak = false;
            blnLateSysPeak = false;
        }

        bool blnHalosystolic = false;
        bool blnPansystolic = false;
        bool blnMidsystolic = false;

        if (blnPlateuShaped)
        {
            blnHalosystolic = chkHalosystolic.Checked;
            blnPansystolic = chkPansystolic.Checked;
            blnMidsystolic = chkMidsystolic.Checked;
        }
        else
        {
            blnHalosystolic = false;
            blnPansystolic = false;
            blnMidsystolic = false;
        }

        bool blnDescrescendo = false;
        bool blnRumble = false;

        if (blnDiastolicMurmur)
        {
            blnDescrescendo = chkDecrescendo.Checked;
            blnRumble = chkRumble.Checked;
            blnMitralOpenSnap = chkMitralOpeningSnap.Checked;
        }
        else
        {
            blnDescrescendo = false;
            blnRumble = false;
            blnMitralOpenSnap = false;
        }

        if (!string.IsNullOrEmpty(TxtHF.Text.Trim()))
        {
            intLVEF = Val_int(TxtHF.Text);
            intTestCHF = intLVEF;
        }
        else if (!string.IsNullOrEmpty(txtCHF.Text.Trim()))
        {
            intLVEF = Val_int(txtCHF.Text);
            intTestCHF = intLVEF;
        }
        else
        {
            intLVEF = Val_int(txtEF.Text);
            intTestCHF = intLVEF;
        }

        //Dim intOrthostaticSBP As Integer = Val(txtOrthostaticSBP.Text)
        //Dim blnSymptomatic As Boolean = chkSymptomatic.Checked


        Calc objCalc = new Calc();

        string strRet = objCalc.Calculate(intTestAge, intTestGender, intTestCholesterol, intTestTRG, intTestLDL, intTestHDL, intTestLPA, dblTestHomo, intTestCCS, intTestSBP,
        intTestDBP, intTestWaist, intTestBMI, blnTestAsianCode, dblTestMicro, blnTestSmoke, blnTestSBPTreated, intTestStatin, blnTestFamily, blnCKD, blnTestSAH, blnaldo,
        blnrenovas, blnFMD, blnOSA, strTestTargetHdl, strTestTargetLdl, strTargetSBP, strTargetDBP, dblTestHBA1C, blnTestDM, blnTestDM1, blngestational, blnTestCHD,
        blnTestSCD, blnCEA, blnTestACS, blnTestMI, blnTestPVD, intTestGlucose, intCRP, blnTestAAA, blnthoracic, blnthoracoabdominal, blnTestCurrently, blnTestLVt,
        blnTestHF, intTestTotalCCS, intTestCHF, intTestHR, blnTestEd, blnTestNSVT, blnTestEKG, intTestGFR, blnAF, blnTestPrevious, intPrevious,
        intCMP, blnTestThyro, blnTestHypoThyro, blnCerebralVenous, blnRetinopathy, blnTestNonischemicCMP, blnTestIschemicCMP, blnTestNYHA1, blnTestNYHA2, blnTestNYHA3, blnTestNYHA4,
        blnTestA, blnTestB, blnTestC, blnTestD, blnTestSYNCOPE, intQRS, intQRSAx, intQTc, intPRduration, blnPCI, blnStenusis, blnHistCardArrest,
        blnFamHistSuddenDeath, blnLVHGreater30, blnAbnormBP, blnLVOT, intdur, blnIE, blnLPVT, blnRPVT, blnSinusNodeDysfunction, blnAVNodeDisease,
        blnVasoDepressorSyncope, blnCarotidSinusHypersensitivity, blnPosturalOrthostatisTac, blnAutonomicFailureSyndrome, blnCardiotoxins, blnFamHXofCMP, blnvalvular, blnSuspectedRVDysplasia,
        blnabnormq, blnFreqVentPac, blnThromboembolic, blnDVTprevention, blnDeepVenousThrombosis, blnDeepVenous, blnPE, blnHeparinIndThrom, blnNoncardioCVA, blnArterialIschemic,
        blnPFO, blnAorticArchAth, blnCalcificAortic, blnMitralAnnular, blnMVP, intApoB, blnApoA1, blnWPW, blnatrialfib, blnLBBB, dblCreatinine, blnBrugada, blnLongQT,
        blnsarc, blngeneral, blnurology, blnknee, blnhip, blnHPS, blncancer, blnspinal, blnacute, intNIHSS, blnsaddle, blnseptic, blnotherPE,
        blnacutecva, blnbldrisk, blnRMVD, blnbiop, blnmech, blncage, intLAd, blnsysemb, blnLAthr, blnVKA, blnNoncardioTIA,
        intO2Sat, blnDOE, blnDysp, blnEdema, blnHepato, blnJVD, blnAMS, blnWG, blnfoutpt, blnICD, intNameq,
        blnLVH, dblINR, blnD1, blnnewHF, blnHFPLVF, blnlung, blnlowv, blnLAD, blnCTO, blnLM, intNumVessel, dblSTEMIdur,
        blnDTS, intExTimeMin, intMaxST, intStressSummedScore, intAnginaIndex, blnIVAA, blnIVDI, blnIVHT, blnIVVA, blncon,
        intmonitor, blnAAcon, blnAAbolus, blnAAtitr, intAAmonitor, blnHTcon, blnHTbolus, blnHTtitr, intHTmonitor, blnVAcon,
        blnVAbolus, blnVAtitr, intVAmonitor, blnNIPPV, intosupp, blnult, blnIABP, blnVAD, blnST, blnPEd,
        blnS3, blnPMI, blnS4, blndistant, blnbruit, blnHJR, blnascites, blnMR, blnang24, blnan20, blnASA, blntrop, blnAnAn, blnD2, blnurgcv,
        blndefib, blnPOAA, blnSVT, blnTPM, blnD3, blnD4, blnD5, blnPODI, blnCV, blnalready,
        intweight, blnTA, blnPA, blnNCP, blnunab, blnunable, blnTWI, dblASCVD, blnAA, blnMVT,
        blnPVT, blntors, blnIVT, blnRMVT, blnidioVT, intafd, blnhead, blnepis, blnCP, blnpreh,
        blncom, blnTestischemic, intVO2, intRI, intLDLP, blnpharm, blnshock, blnskinwarm, blnweight, blnBMS, blnDES,
        blnCABG, blnVHD, blnCAA, blnCAC, blnFactorX, blnactivecancer, blnhormone, blninfection, blnrecent, blnreduced, blnRF,
        blnPVTE, blnthrombo, blnbleed, blnCVC, blnICU, blnplt, blnPUD, blnTIA, blnDVTE, blnorthopnea, blnPND, blnpalpitations, blnrheum, blnEcho, blnHC,
        blnmdav, intproBNP, blnminorpro, blnsurgery, blnemergencysurgery, blnIVNPS, blnIVNTG, blnmilrinone, blnclass1, blnclass2,
        blnclass3, blncar, blnace, blnCCB, blnvasodilator, blndiuretic, blnCarvedilol625, blnCarvedilol125, blnCarvedilol25, blnMetoprololER50, blnMetoprololER100,
        blnMetoprololER200, blnBisoprolol25, blnBisoprolol5, blnBisoprolol10, blnfurosemide40, blnfurosemide80, blnfurosemide81, blnbumex1, blnbumex2, blnbumex3,
        blntorsemide20, blntorsemide40, blntorsemide50, blnSymptomatic, blnpreg, blnstatint, blninfreqvent, intOrthostaticSBP, intTestBUN, intMETS,
        intDASI, intDSE, intreversible, intWMA, intMPS, intBNP, blnelective, blnemergency, blnacutely, blnvascular,
        blnSTelevation, blnabnormalBP, blnprolongedST, blnExVT, blnlowriskelec, blnfixed, blnunterpret, blnnewWMA, blnviable, blnliver,
        blnablation, blnflutter, blnTID, blnclass1AA, blnclass3AA, blnchangetrop, blnrefractory, blnangina, blnangina20, blnpositivetrop,
        blnASAuse, blnSTEMI, blnER, intTTS, inturineNa, intsosm, intTestASBP, intTestADBP, intuosm, blnHCTZ,
        blnindapamide, blnchlorthalidone, blnbifascicular, blnLAHB, blnLPHB, blnAPCs, blnJPDs, blnPSVT, blnNSR, blnVpace, blnRBBB, blnIVCD,
        blnLVaneurysm, blnmyocarditis, blnrestrictive, blnRCA, blnanterior, blninferior, blnother, blnautovein, blnautoartery, blnnonauto,
        blncoraneurysm, blncordissection, blncalcified, blnlipidrich, blnLCX, blnparoxysmal, blnpersistent, blnchronicAF, blntypicalAF, blnatypicalAF,
        blnpleur, blnCPbreath, blnvasospasm, blnABI, blnclaudication, blncritical, blnbypass, blnBKbypass, blnHeartMurmur, blnS1Mitral,
        blnS2Aortic, blnP2Pulmonic, blnSystolicMurmur, blnDiastolicMurmur, blnMitralOpenSnap, blnS1MitralLoud, blnS1MitralNormal, blnS1MitralSoft, blnS2AorticLoud, blnS2AorticNormal,
        blnS2AorticSoft, blnP2PulmonicLoud, blnP2PulmonicNormal, blnP2PulmonicSoft, blnCresDecres, blnPlateuShaped, blnEarlyMidSysPeak, blnLateSysPeak, blnHalosystolic, blnPansystolic,
        blnMidsystolic, blnDescrescendo, blnRumble, blnLVeccenticity, blnMPI, blnEnlargedLA, intMP, blnSynStanding, blnSynProSitting, blnSynOther,
        blnSynNausea, blnSynDizziness, blnSynPalpitations, blnSynProCP, intSynMin, intSynSec, blnSynNone, blnSynDisor, blnSynPara, blnSynPosCP, blnSynsporadic, blnSyntrauma,
        blnPrecordial, blnCPSharp, blnCPPressure, blnCPTightness, blnCPSoreness, blnCPLying, blnCPBreathing, blnCPCoughing, blnCPExertion, blnCPPalpation,
        blnCPNTG, blnCPRest, blnCPSitting, blnCPAnalgesics, blnCPJaw, blnCPBack, blnCPArm, blnCPDyspnea, blnCPNausea, blnCPDiaph, blnCPseconds, blnCPminutes,
        blnCPFaint, blnCPpalpitations, blnCPhours, blnCPconstant, blnescTA, blnTearing, blnCPlyingside, blnrub, blnbreath, blnabdominal, intDiffInSBP, blnchestxray, blnIVSshift,
        blnmitralE, blnhepatic, blnEnlargedRA, blnEnlargedRV, blnmidsystolicn, dblTRJ, blnRPLE, blnLPLE, blnpuledema, blnenlarcar,
        blnWidened, blnpneumothorax, blninfiltrate, blnconsolidation, blnpericardial, blnRWMA, blnhemoptysis, blnunilateral, blnulcer, blnswelling, blnSynneck, blnSynaura, blnSynfatigue,
        blnSynpallor, intprocedure, blnfirstdegree, blnsecondegree, blnthirdegree, blntype1, blntype2, blnSynposture, blnsquat, blntrop2, blntrop3, blnnonspesific, blnprior, blnpriorASA,
        blnS1Tricuspid, blnS1TricuspidLoud, blnS1TricuspidNormal, blnS1TricuspidSoft, blnsystolicclick, blnejectionsound, blnDMmono, blnDMpoly, blnDMautonom, blnDMNP, blnDMCKD, blnDMother,
        blnDMarthro, blnDMskin, blnDMoral, blnDMangio, blnDMgangrene, blnDMothercirc, blnDMhypo, blnDMhypocoma, blnDMhyper, blnDMothercomp, blnDMunspec, blnDMwithout, blnlisinopril5,
        blnlisinopril10, blnlisinopril20, blnlocaledema, blndiffusedema, blnpitting, blnnonpitting, blnmalaise, blnAbspasm, blnAbaching, blnAbcolic, blnAbsharp, blnumbilical, blnepigastric, blnsuprapubic,
        blndistention, blnrebound, blnpulsatile, blnbowelsounds, blnpsaos, blnMcburney, blnRovsing, blnMurphy, blnobturator, blncarnett, blncostovertebral, blnskin, blnvomiting, blnanorexia,
        blnbowel, blnpostoperative, blnfamilyhistoryAA, blnurinary, blnobgyn, blnhematuria, blnechymosis, blncelulitis, blnherpes, blnhenoch, blnicterus, blncaput, blnvomitfirst, blnpainfirst,
        blnpalday, blnpalweek, blnpalmonth, blnpalyear, blnextrasys, blnanxiety, blntachycardic, blnpulsation, blnpalsyncope, blnpalangina, blnpalneck, blnpalpolyuria, blnpalposition, blnpalsudden,
        blnpalexertion, blnpalpostexertion, blnhypovolemia, blnallergen, blninfectious, blnpharmacologic, blnspinalcord,blnlocalized, blndiffuse,intpalseconds,intpalminutes, intpalhours,
        blnpalNSR, blnpalregRR, blnpalirregRR,blncentral, blnperipheral,blnvagalabrupt, blnvagalgradual,blnvagalnoresponse, dblcr, dblcrinc, dblurine, blnsediment, blnrenalimage, blnhistology, blnRBC,
        blnRBCcast, blnWBCcast, blngranular, blnoval, blnCyanosis, blnrhonchi, blnpercussion, blnthorax, blnasthma, blnILD, blnCOPD,intFEVP,intFVCP,intDLCO, dblFEV1,
        intasymptoms,intnocturnal,intSABA,blninterference, blnnone, blnminor, blnsome, blnsignificant, blncough, blnsputum, blnsinus, blnlosartan25, blnlosartan50,blnlosartan100);

        objCalc.Calc_Tansyon();

        Session["lblTargetHdl"] = objCalc.TargetHdl;
        Session["lblTargetLdl"] = objCalc.TargetLdl;
        Session["strFRS"] = objCalc.strFRS;
        Session["lblTargetSBP"] = objCalc.TargetSBP;
        Session["lblTargetDBP"] = objCalc.TargetDBP;
        Session["Niaspan1"] = objCalc.strNiaspan;
        Session["Niaspan2"] = objCalc.strNiaspan2;
        Session["Niaspan3"] = objCalc.strNiaspan3;
        Session["Niaspan4"] = objCalc.strNiaspan4;
        Session["Niaspan5"] = objCalc.strNiaspan5;

        Session["strStatin"] = objCalc.strStatin;
        Session["strischemic"] = objCalc.strischemic;
        Session["strValvular"] = objCalc.strvalvular;
        Session["strStatinIntolerance"] = objCalc.strStatinIntolerance;
        Session["strCHFandAF"] = objCalc.strCHFandAF;
        Session["strreferral"] = objCalc.strreferral;
        Session["strRef1"] = objCalc.strRef1;
        Session["strRef2"] = objCalc.strRef2;

        if (checkCHFetyology == false)
        {
            Session["Output_Tansyon"] = "   " + objCalc.Output_Tansyon;
        }
        else
        {
            Session["Output_Tansyon"] = objCalc.Output_Tansyon;
        }
        // Session["Output_Tansyon"] = objCalc.Output_Tansyon
        Session["outputSAH"] = objCalc.outputSAH;
        Session["outputCOAG"] = objCalc.outputCOAG;
        Session["outputLifevest"] = objCalc.outputLifevest + objCalc.strProbOfDeath;
        Session["outputICD"] = objCalc.outputICD;
        Session["outputContrain"] = objCalc.outputContrain;
        //Session["colorflag"] = "yellow";

        if (blnDTS == true)
        {
            if (intExTimeMin > 0)
            {
                if (objCalc.intTTS >= 5)
                {
                    TxtTTS.Text = Convert.ToString(objCalc.intTTS);// + "   Low Risk";
                }
                else if (objCalc.intTTS > -11)
                {
                    TxtTTS.Text = Convert.ToString(objCalc.intTTS);// + "   Moderate Risk";
                }
                else
                {
                    TxtTTS.Text = Convert.ToString(objCalc.intTTS);// + "   High Risk";
                }
            }
            else
            {
                TxtTTS.Text = "";
            }
        }
        else
        {
            TxtTTS.Text = "";
        }



        //Populate Input Summary

        System.Text.StringBuilder ins = new System.Text.StringBuilder();
        System.Text.StringBuilder tmpsb = new System.Text.StringBuilder();
        System.Text.StringBuilder tmpsb2 = new System.Text.StringBuilder();
        System.Text.StringBuilder tmpsb3 = new System.Text.StringBuilder();
        System.Text.StringBuilder tmpsb4 = new System.Text.StringBuilder();

        ins.AppendFormat("{0}, {1} years old, {2}", txtFirstName.Text, txtAge.Text, (rblGender.SelectedValue == "1" ? "Male" : "Female"));
        if (!string.IsNullOrEmpty(txtWeight.Text.Trim()))
        {
            ins.AppendFormat(", {0} kg", txtWeight.Text.Trim());
        }

        if (!string.IsNullOrEmpty(txtASBP.Text.Trim()))
        {
            ins.AppendFormat(", Amb SBP:{0}", txtASBP.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtADBP.Text.Trim()))
        {
            ins.AppendFormat(", Amb DBP:{0}", txtADBP.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtWaist.Text.Trim()))
        {
            ins.AppendFormat(", Waist Circumference:{0} inches", txtWaist.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtBMI.Text.Trim()))
        {
            ins.AppendFormat(", BMI:{0}", txtBMI.Text.Trim());
        }
        if (chkAsianCode.Checked)
        {
            ins.Append(", African American");
        }


        if (ChknewHF.Checked)
        {
            tmpsb.Append(" The patient presents with new onset dyspnea, suspected heart failure.");
        }
        if (ChKHFPLVF.Checked)
        {
            tmpsb.Append(" The patient has known heart failure.");
        }
        if (Chklung.Checked)
        {
            tmpsb.Append(" The patient has known lung disease.");
        }
        if (ChkD5.Checked)
        {
            tmpsb.Append(" The patient is seen as outpatient.");
        }
        if (ChkD4.Checked)
        {
            tmpsb.Append(" The patient is seen in emergency Room.");
        }
        if (ChkD1.Checked)
        {
            tmpsb.Append(" The patient is hospitalized for heart failure, day 1.");
        }
        if (ChkD2.Checked)
        {
            tmpsb.Append(" The patient is hospitalized for heart failure, day 2.");
        }
        if (ChkD3.Checked)
        {
            tmpsb.Append(" The patient is hospitalized for heart failure, day 3.");
        }
        if (!string.IsNullOrEmpty(txtdur.Text.Trim()))
        {
            tmpsb.AppendFormat(" HF Diagnosis duration(s) is: {0} weeks.", txtdur.Text.Trim());
        }
        if (ChkDOE.Checked)
        {
            tmpsb.Append(" The patient complains of dyspnea");
            if (chkclass1.Checked)
            {
                tmpsb2.Append(" on strenous exertion");
            }
            if (chkclass2.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" on more than daily routine activities");
                Session["colorflag"] = "GREEN";
            }
            if (chkclass3.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" on daily routine / minimal activities");
                
                Session["colorflag"] = "RED";
            }

            if (tmpsb2.Length > 0)
            {
                tmpsb.Append(" ");
                tmpsb.Append(tmpsb2.ToString());
                tmpsb.Append(". ");
            }
            tmpsb2.Remove(0, tmpsb2.Length);
         }
         if (Chkdysp.Checked)
         {
            tmpsb.Append(" The patient complains of dyspnea at rest.");
            Session["colorflag"] = "RED";
         }
        if (ChkWeight.Checked)
        {
            tmpsb.Append(" He also complains of weight gain.");
        }
        if (Chkfoutpt.Checked)
        {
            tmpsb.Append(" The patient has history of previous heart failure hospitalization.");
        }
        else
        {
            tmpsb.Append(" The patient has no previous history of heart failure hospitalization. ");
        }
        if (chkICD.Checked)
        {
            tmpsb.Append(" He is also status post ICD. ");
        }
        if (ChkNonischemicCMP.Checked)
        {
            tmpsb.Append(" The patient has known DCM/ Nonischemic cardiomyopathy. ");
        }
        if (ChkIschemic.Checked || chkMI.Checked)
        {
            tmpsb.Append(" The patient has known ischemic cardiomyopathy. ");
        }
        if (ChkCardiotoxins.Checked)
        {
            tmpsb.Append(" The patient has history of cardiotoxins exposure.");
        }
        if (ChkFamHXofCMP.Checked)
        {
            tmpsb.Append("The patient has family history of Familial CMP Associated with Sudden Death");
        }
        if (Chkvalvular.Checked)
        {
            tmpsb.Append(" The patient has history of Valvular Heart Disease.");
        }
        if (ChkSuspectedRVDysplasia.Checked)
        {
            tmpsb.Append("The patient has history of RV Dysplasia.");
        }
        if (Chkmyocarditis.Checked)
        {
            tmpsb.Append(" The patient has myocarditis.");
        }
        if (Chkrestrictive.Checked)
        {
            tmpsb.Append(" The patient has peripartum cardiomyopathy.");
        }
        if (ChkIschemicCMP.Checked)
        {
            tmpsb.Append( "The patient has history of Hypertrophic cardiomyopathy.");
        }
        if (chkasthma.Checked)
        {
            tmpsb.Append("The patient has history of asthma.");

            if (chkinterference.Checked)
            {
                tmpsb.Append("The interference with daily activities is :");
                if (chknone.Checked)
                {
                    tmpsb.Append(" none.");
                }
                if (chkminor.Checked)
                {
                    tmpsb.Append(" minor.");
                }
                if (chksome.Checked)
                {
                    tmpsb.Append(" some.");
                }
                if (chksignificant.Checked)
                {
                    tmpsb.Append(" significant.");
                }
            }
            if (!string.IsNullOrEmpty(txtasymptoms.Text.Trim()))
            {
                tmpsb.AppendFormat(" Frequency of symptoms is: {0} times a week. ", txtasymptoms.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtnocturnal.Text.Trim()))
            {
                tmpsb.AppendFormat(" Nocturnal awakening is: {0} times a week.", txtnocturnal.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtSABA.Text.Trim()))
            {
                tmpsb.AppendFormat(" Short acting bronhodilators are used: {0} times a week.", txtSABA.Text.Trim());
            }
        }

        if (chkILD.Checked)
        {
            tmpsb.Append("The patient has history of interstitial lung disease.");
        }
        if (chkCOPD.Checked)
        {
            tmpsb.Append("The patient has history of COPD.");
        }
        if (!string.IsNullOrEmpty(txtFEV1.Text.Trim()))
        {
            tmpsb.AppendFormat(" FEV1 is: {0} L/sec.", txtFEV1.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtFEV1.Text.Trim()))
        {
            tmpsb.AppendFormat(" FEV1 is: {0} % predicted.", txtFEVP.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtFEVP.Text.Trim()))
        {
            tmpsb.AppendFormat(" FVCP is: {0} % predicted.", txtFVCP.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtDLCO.Text.Trim()))
        {
            tmpsb.AppendFormat(" DLCO is: {0} % predicted.", txtDLCO.Text.Trim());
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b>History of present illness:</b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }


        if (chkTA.Checked)
        {
            tmpsb.Append(" The patient reports substernal chest pain, pressure, exertional, associated with dyspnea, relieved or alleviated with NTG / rest. ");
            if (chkescTA.Checked)
            {
                tmpsb.Append(" The CP is escalating in frequency, duration, severity.");
                Session["colorflag"] = "RED";
            }
            if (chkangina.Checked)
            {
                tmpsb.Append(" The angina was experienced twice in 24 Hours.");
                Session["colorflag"] = "RED";
            }
            if (chkangina20.Checked)
            {
                tmpsb.Append(" The patient had CP episode lasting 20 minutes.");
                Session["colorflag"] = "RED";
            }
            if (chkASAuse.Checked)
            {
                tmpsb.Append(" The patient has used ASA within past 1 week. ");
            }
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b>History of present illness:</b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }

        if (chkPA.Checked)
        {
            tmpsb.Append(" The patient reports substernal chest pain, pressure, exertional, associated with dyspnea, relieved or alleviated with NTG / rest. ");
            if (chkangina.Checked)
            {
                tmpsb.Append(" The angina was experienced twice in 24 Hours.");
                Session["colorflag"] = "RED";
            }
            if (chkangina20.Checked)
            {
                tmpsb.Append(" The patient had CP episode lasting 20 minutes.");
                Session["colorflag"] = "RED";
            }
            if (chkASAuse.Checked)
            {
                tmpsb.Append(" The patient has used ASA within past 1 week. ");
            }
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b>History of present illness:</b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }

        if (chkNCP.Checked)
        {
            if (chkpleur.Checked)
            {
                tmpsb2.Append(" The patient reports side chest pain.");
                Session["colorflag"] = "RED";
            }
            if (chkCPbreath.Checked)
            {
                tmpsb2.Append(" The patient reports intercostal chest pain.");
                Session["colorflag"] = "GREEN";
            }
            if (chkPrecordial.Checked)
            {
                tmpsb2.Append(" The patient reports precordial chest pain.");
                Session["colorflag"] = "RED";
            }
            if (chkCPSharp.Checked)
            {
                tmpsb3.Append(" sharp");
            }
            if (chkCPPressure.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(", ");
                }
                tmpsb3.Append("pressure");
            }
            if (chkCPTightness.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(", ");
                }
                tmpsb3.Append("tightness");
            }
            if (chkCPSoreness.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(", ");
                }
                tmpsb3.Append("soreness");
            }
            if (Chktearing.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(", ");
                }
                tmpsb3.Append("heaviness");
            }

            if (tmpsb3.Length > 0)
            {
                tmpsb2.Append(" The quality of CP is ");
                tmpsb2.Append(tmpsb3.ToString());
                tmpsb3.Remove(0, tmpsb3.Length);
            }
            
            if (chkCPLying.Checked)
            {
                tmpsb3.Append(" lying");
            }
            if (chkCPBreathing.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(", ");
                }
                tmpsb3.Append(" breathing");
            }
            if (chkCPCoughing.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(", ");
                }
                tmpsb3.Append(" coughing");
            }
            if (chkCPExertion.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(", ");
                }
                tmpsb3.Append(" exertion");
            }
            if (chkCPPalpation.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(", ");
                }
                tmpsb3.Append(" palpation");
            }
            if (tmpsb3.Length > 0)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(", ");
                }
                tmpsb2.Append(" aggravated by ");
                tmpsb2.Append(tmpsb3.ToString());
                tmpsb3.Remove(0, tmpsb3.Length);
            }
            
            if (chkCPPalpation.Checked==false && chkCPExertion.Checked==false && chkCPCoughing.Checked==false && chkCPBreathing.Checked==false && chkCPLying.Checked==false )
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(", ");
                }
                tmpsb2.Append(" denies aggravating factors");
                tmpsb2.Append(tmpsb3.ToString());
            }

            if (chkCPNTG.Checked)
            {
                tmpsb3.Append("nitrates");
            }
            if (chkCPRest.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(", ");
                }
                tmpsb3.Append("rest");
            }
            if (chkCPSitting.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(", ");
                }
                tmpsb3.Append("sitting-Up");
            }
            if (chkCPlyingside.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(", ");
                }
                tmpsb3.Append("lying on-side");
            }
            if (chkCPAnalgesics.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(", ");
                }
                tmpsb3.Append("analgesics");
            }
            if (chkCPAnalgesics.Checked==false && chkCPAnalgesics.Checked==false && chkCPlyingside.Checked==false && chkCPSitting.Checked==false && chkCPRest.Checked==false && chkCPNTG.Checked==false)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(", ");
                }
                tmpsb3.Append("no known factors");
            }
            if (tmpsb3.Length > 0)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(", ");
                }
                tmpsb2.Append("relieved by ");
                tmpsb2.Append(tmpsb3.ToString());
                tmpsb3.Remove(0, tmpsb3.Length);
            }

            if (chkCPJaw.Checked)
            {
                tmpsb3.Append("jaw");
            }
            if (chkCPBack.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(", ");
                }
                tmpsb3.Append("back");
            }
            if (chkCPArm.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(", ");
                }
                tmpsb3.Append("arm");
            }
            if (chkCPArm.Checked==false && chkCPBack.Checked==false && blnCPJaw==false)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(", ");
                }
                tmpsb3.Append(" no other body parts");
            }
            if (tmpsb3.Length > 0)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(", ");
                }
                tmpsb2.Append("radiating to ");
                tmpsb2.Append(tmpsb3.ToString());
                tmpsb3.Remove(0, tmpsb3.Length);
            }
            
            if (chkCPDyspnea.Checked)
            {
                tmpsb3.Append("dyspnea");
            }
            if (chkCPNausea.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(", ");
                }
                tmpsb3.Append("nausea");
            }
            if (chkCPDiaph.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(", ");
                }
                tmpsb3.Append("diaphoresis");
            }
            if (chkCPFaint.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(", ");
                }
                tmpsb3.Append("faintness");
            }
            if (chkCPpalpitations.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(", ");
                }
                tmpsb3.Append("faintness");
            }
            if (chkCPFaint.Checked==false&& chkCPDiaph.Checked==false&& chkCPNausea.Checked==false&& chkCPDyspnea.Checked==false && chkCPpalpitations.Checked == false)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(", ");
                }
                tmpsb3.Append(" no other symptoms");
            }
            if (tmpsb3.Length > 0)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(", ");
                }
                tmpsb2.Append("associated with ");
                tmpsb2.Append(tmpsb3.ToString());
                tmpsb3.Remove(0, tmpsb3.Length);
            }
            
            if (chkCPseconds.Checked)
            {
                tmpsb3.Append(" The CP lasts seconds.");
            }
            if (chkCPminutes.Checked)
            {
                tmpsb3.Append(" The CP lasts minutes.");
            }
            if (chkCPhours.Checked)
            {
                tmpsb3.Append(" The CP lasts hours.");
            }
            if (chkCPconstant.Checked)
            {
                tmpsb3.Append(" The CP is constant.");
            }
            if (tmpsb3.Length > 0)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(", ");
                }
                tmpsb2.Append(tmpsb3.ToString());
                tmpsb3.Remove(0, tmpsb3.Length);
            }
            
            if (chkunable.Checked)
            {
                tmpsb2.Append(" The patient is unable to exercise. ");
            }
        }
        if (tmpsb2.Length > 0)
        {
            ins.Append("<br /><b>History of present illness:</b><br />");
            ins.Append(tmpsb2.ToString());
            tmpsb2.Remove(0, tmpsb2.Length);
        }

        if (ChkTestSyncope.Checked)
        {
            if (chkSynsporadic.Checked)
            {
                tmpsb3.Append("sporadically");
            }
            if (chkSynStanding.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" upon prolonged standing");
            }
            if (chkSynProSitting.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" during exertion");
                Session["colorflag"] = "RED";
            }
            if (chkSynOther.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" provoked by sudden noise or emotion");

                if (chkSynProCP.Checked==false && chkSynNone.Checked==false && chkSyntrauma.Checked == false)
                {
                    Session["colorflag"] = "GREEN";
                }
            }
            if (chkSynneck.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" after neck movements.");
                if (chkSynProCP.Checked == false && chkSynNone.Checked==false && chkSyntrauma.Checked==false)
                {
                    Session["colorflag"] = "GREEN";
                }
            }
            
            if (tmpsb3.Length > 0)
            {
                tmpsb2.Append(" Patient reports syncopal episodes ");
                tmpsb2.Append(tmpsb3.ToString());
                tmpsb2.Append(".");
                tmpsb3.Remove(0, tmpsb3.Length);
            }

            if (chkSynDizziness.Checked)
            {
                tmpsb3.Append(" dizziness");
            }
            if (chkSynNausea.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" nausea");
            }
            if (chkSynPalpitations.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" palpitation");
            }
            if (chkSynProCP.Checked)
            {
                if (tmpsb3.Length > 0)
                { 
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" chest pain");
                Session["colorflag"] = "RED";
            }
            if (tmpsb3.Length > 0)
            {
                tmpsb2.Append(" Prodromal symtoms include ");
                tmpsb2.Append(tmpsb3.ToString());
                tmpsb2.Append(".");
                tmpsb3.Remove(0, tmpsb3.Length);
            }

            if (!string.IsNullOrEmpty(txtSynMin.Text.Trim()))
            {
                tmpsb3.AppendFormat(" {0} minutes", txtSynMin.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtSynSec.Text.Trim()))
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.AppendFormat(" {0} seconds", txtSynSec.Text.Trim());
            }
            if (tmpsb3.Length > 0)
            {
                tmpsb2.Append(" Syncope Duration is ");
                tmpsb2.Append(tmpsb3.ToString());
                tmpsb2.Append(".");
                tmpsb3.Remove(0, tmpsb3.Length);
            }

            if (chkSynNone.Checked)
            {
                tmpsb3.Append(" cyanosis is observed");
                Session["colorflag"] = "RED";
            }
            if (chkSynpallor.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" pallor is observed");
            }
            if (chkSynposture.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" unusual posture is observed");
            }
            if (tmpsb3.Length > 0)
            {
                tmpsb2.Append(" During syncope ");
                tmpsb2.Append(tmpsb3.ToString());
                tmpsb2.Append(".");
                tmpsb3.Remove(0, tmpsb3.Length);
            }

            if (chkSynDisor.Checked)
            {
                tmpsb3.Append(" disorientation");
            }
            if (chkSynPara.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append("paralysis");
            }
            if (chkSynPosCP.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" chest pain");
            }
            if (chkSynaura.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" aura");
            }
            if (chkSynfatigue.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" prolonged fatigue");
            }
            if (chkSyntrauma.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" post traumatic injury");
                Session["colorflag"] = "RED";
            }
            if (tmpsb3.Length > 0)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Postictal symptoms include ");
                tmpsb2.Append(tmpsb3.ToString());
                tmpsb2.Append(".");
                tmpsb3.Remove(0, tmpsb3.Length);
            }
            if (tmpsb2.Length > 0)
            {
                ins.Append("<br /><b>History of present illness:</b><br />");
                ins.Append(tmpsb2.ToString());
                tmpsb2.Remove(0, tmpsb2.Length);
            }
        }
        if (chkpalday.Checked|| chkpalweek.Checked || chkpalmonth.Checked || chkpalyear.Checked)
        {
            if (chkpalday.Checked)
            {
                tmpsb3.Append(" daily palpitations");
            }
            if (chkpalweek.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append("");
                }
                tmpsb3.Append(" weekly palpitations");
            }
            if (chkpalmonth.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append("");
                }
                tmpsb3.Append(" monthly palpitations");
                Session["colorflag"] = "RED";
            }
            if (chkpalyear.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" yearly palpitations");

                
            }
           

            if (tmpsb3.Length > 0)
            {
                tmpsb2.Append(" Patient complains of ");
                tmpsb2.Append(tmpsb3.ToString());
                tmpsb2.Append(".");
                tmpsb3.Remove(0, tmpsb3.Length);
            }

            if (chkextrasys.Checked)
            {
                tmpsb3.Append(" extra heart beats, 'heart beats pause and start' ");
            }
            if (chktachycardic.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" fluttering in chest, bat wing");
            }
            if (chkanxiety.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" anxiety");
            }
            if (chkpulsation.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" regular strong heart beats");
                Session["colorflag"] = "RED";
            }
            if (tmpsb3.Length > 0)
            {
                tmpsb2.Append(" The symptoms are felt as; ");
                tmpsb2.Append(tmpsb3.ToString());
                tmpsb2.Append(".");
                tmpsb3.Remove(0, tmpsb3.Length);
            }

            if (!string.IsNullOrEmpty(txtpalseconds.Text.Trim()))
            {
                tmpsb3.AppendFormat(" {0} seconds", txtpalseconds.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtpalminutes.Text.Trim()))
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.AppendFormat(" {0} minutes", txtSynSec.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtpalhours.Text.Trim()))
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.AppendFormat(" {0} hours", txtSynSec.Text.Trim());
            }
            if (tmpsb3.Length > 0)
            {
                tmpsb2.Append(" Symptoms duration is ");
                tmpsb2.Append(tmpsb3.ToString());
                tmpsb2.Append(".");
                tmpsb3.Remove(0, tmpsb3.Length);
            }

            if (chkpalNSR.Checked)
            {
                tmpsb3.Append(" NSR ");
                Session["colorflag"] = "RED";
            }
            if (chkpalregRR.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" other rhtym than sinus with regular RR intervals");
            }
            if (chkpalirregRR.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" other rhtym than sinus with irregular RR intervals");
            }
            if (tmpsb3.Length > 0)
            {
                tmpsb2.Append(" EKG findings during syncope are: ");
                tmpsb2.Append(tmpsb3.ToString());
                tmpsb2.Append(".");
                tmpsb3.Remove(0, tmpsb3.Length);
            }

           
            if (chkpalsyncope.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append("syncope");
            }
            if (chkpalangina.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(" ");
                }
                tmpsb3.Append(" chest pain");
            }
            if (chkpalneck.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append("");
                }
                tmpsb3.Append(" pulsations in the neck");
            }
            if (chkpalpolyuria.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append("");
                }
                tmpsb3.Append(" polyuria");
            }
            
            if (tmpsb3.Length > 0)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append("");
                }
                tmpsb2.Append(" Other symptoms during palpitations include ");
                tmpsb2.Append(tmpsb3.ToString());
                tmpsb2.Append(".");
                tmpsb3.Remove(0, tmpsb3.Length);
            }
            if (chkpalsudden.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append("sudden onset");
            }
            if (chkpalposition.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" position change");
            }
            if (chkpalexertion.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" exertion");
            }
            if (chkpalpostexertion.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" post exertion");
            }

            if (tmpsb3.Length > 0)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append("");
                }
                tmpsb2.Append(" The onset and triggering factors are: ");
                tmpsb2.Append(tmpsb3.ToString());
                tmpsb2.Append(".");
                tmpsb3.Remove(0, tmpsb3.Length);
            }
            if (tmpsb2.Length > 0)
            {
                ins.Append("<br /><b>History of present illness:</b><br />");
                ins.Append(tmpsb2.ToString());
                tmpsb2.Remove(0, tmpsb2.Length);
            }
        }
        if (chkAbsharp.Checked||chkAbcolic.Checked||chkAbspasm.Checked)
        {
            if (blndiffuse) {
                if (chkAbsharp.Checked)
                {
                    if (tmpsb3.Length > 0)
                    {
                        tmpsb3.Append(",");
                    }
                    tmpsb3.Append(" diffuse and sharp");
                }
                if (chkAbcolic.Checked)
                {
                    if (tmpsb3.Length > 0)
                    {
                        tmpsb3.Append(",");
                    }
                    tmpsb3.Append(" diffuse and colicky");

                }
                if (chkAbspasm.Checked)
                {
                    if (tmpsb3.Length > 0)
                    {
                        tmpsb3.Append(",");
                    }
                    tmpsb3.Append(" diffuse and spasm like");
                }

            }
            else
           {

                if (chkAbsharp.Checked)
                {
                    if (tmpsb3.Length > 0)
                    {
                        tmpsb3.Append(",");
                    }
                    tmpsb3.Append(" localized and sharp");
                }
                if (chkAbcolic.Checked)
                {
                    if (tmpsb3.Length > 0)
                    {
                        tmpsb3.Append(",");
                    }
                    tmpsb3.Append(" localized and colicky");

                }
                if (chkAbaching.Checked)
                {
                    if (tmpsb3.Length > 0)
                    {
                        tmpsb3.Append(",");
                    }
                    tmpsb3.Append(" localized and spasm like");
                }
            }
            if (tmpsb3.Length > 0)
            {
                tmpsb2.Append(" Patient reports abdominal pain, ");
                tmpsb2.Append(tmpsb3.ToString());
                tmpsb2.Append(".");
                tmpsb3.Remove(0, tmpsb3.Length);
            }

            if (chkepigastric.Checked)
            {
                tmpsb3.Append(" epigastric");
            }
            if (chkperiumbilical.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(" ");
                }
                tmpsb3.Append(" periumbilical");
            }
            if (chksuprapubic.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(" ");
                }
                tmpsb3.Append(" suprapubic");
            }
            if (chkAbspasm.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(" ");
                }
                tmpsb3.Append(" referred to left shoulder");
               
            }
            if (tmpsb3.Length > 0)
            {
                tmpsb2.Append(" Location is ");
                tmpsb2.Append(tmpsb3.ToString());
                tmpsb2.Append(".");
                tmpsb3.Remove(0, tmpsb3.Length);
            }

            

            if (chkvomitfirst.Checked)
            {
                tmpsb3.Append(" vomiting preceding pain");
                Session["colorflag"] = "RED";
            }
            if (chkpainfirst.Checked)
            {
                tmpsb3.Append(" vomiting proceding pain");
                Session["colorflag"] = "RED";
            }
            if (chkanorexia.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" anorexia");
            }
            if (chkbowel.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" change in bowel habit");
            }
           

            if (chkpostoperative.Checked)
            {

                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" post operative status");
            }
            if (chkhematuria.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append("hematuria");
            }
            if (chkurinary.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" change in urinary habit");
            }
            if (chkobgyn.Checked)
            {
                if (tmpsb3.Length > 0)
                {
                    tmpsb3.Append(",");
                }
                tmpsb3.Append(" ob&gyn history");
            }
           
            if (tmpsb3.Length > 0)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append("");
                }
                tmpsb2.Append(" Associated symptoms include: ");
                tmpsb2.Append(tmpsb3.ToString());
                tmpsb2.Append(".");
                tmpsb3.Remove(0, tmpsb3.Length);
            }
            if (tmpsb2.Length > 0)
            {
                ins.Append("<br /><b>History of present illness:</b><br />");
                ins.Append(tmpsb2.ToString());
                tmpsb2.Remove(0, tmpsb2.Length);
            }
        }
       

        if (chkTA.Checked==false && chkPA.Checked==false && chkNCP.Checked==false && ChkDOE.Checked==false&& Chkdysp.Checked==false &&ChkTestSyncope.Checked==false )
        {
            tmpsb.Append(" The patient returns for follow up, denies dyspnea, chest pain, faintness, syncope. ");
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b>History of present illness:</b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }

        //ins.AppendFormat("<br />SBP:{0}, DBP:{1}", txtSBP.Text.Trim(), txtDBP.Text.Trim());
        if (chkSAH.Checked)
        {
            tmpsb.Append(" Hypertension.");
        }
        if (chkalready.Checked || intTestLDL>100 || intTestTRG>150)
        {
            tmpsb.Append(" Hyperlipidemia.");
        }
        if (chkDM.Checked)
        {
            tmpsb.Append(" Type 2 Diabetes.");
        }
        if (chkDM1.Checked)
        {
            tmpsb.Append(" Type 1 Diabetes.");
        }
        if (chkCKD.Checked && intTestGFR>90 && dblTestMicro>30 && blnDMangio==false && blnTestDM1==false)
        {
            tmpsb.Append(" Chronic kidney disease, stage 1.");
        }
        if (chkCKD.Checked && intTestGFR > 59 && intTestGFR < 90 )
        {
            tmpsb.Append(" Chronic kidney disease, stage 2.");
        }
        if (chkCKD.Checked && intTestGFR > 44 && intTestGFR <60 )
        {
            tmpsb.Append(" Chronic kidney disease, stage 3.");
        }
        if (chkCKD.Checked && intTestGFR > 29 && intTestGFR < 45)
        {
            tmpsb.Append(" Chronic kidney disease, stage 3.");
        }
        if (chkCKD.Checked && intTestGFR > 14 && intTestGFR < 30)
        {
            tmpsb.Append(" Chronic kidney disease, stage 4.");
        }
        if (chkCKD.Checked && intTestGFR < 15)
        {
            tmpsb.Append(" Chronic kidney disease, stage 5.");
        }
        if (ChkOSA.Checked)
        {
            tmpsb.Append(" Obstructive sleep apnea.");
        }
        if (Chkaldo.Checked)
        {
            tmpsb.Append(" Hyperaldesteronism.");
        }
        if (ChkFMD.Checked)
        {
            tmpsb.Append(" Pheocromocytoma.");
        }
        if (Chkrenovas.Checked)
        {
            tmpsb.Append(" Renal artery stenosis.");
        }
        if (ChkchronicAF.Checked)
        {
            tmpsb.Append(" Chronic atrial fibrillation.");
        }
        if (Chkparoxysmal.Checked)
        {
            tmpsb.Append(" Paroxysmal atrial fibrillation.");
        }
        if (Chkpersistent.Checked)
        {
            tmpsb.Append(" Persistent atrial fibrillation.");
        }
        if (ChkNonischemicCMP.Checked)
        {
            if (intTestCHF<40 && intTestCHF>0)
            {
                tmpsb.Append(" Dilated cardiomyopathy.");
            }
            else if (chkSAH.Checked)
            {
                tmpsb.Append(" Hypertensive heart disease.");
            }
        }
        if (ChkIschemicCMP.Checked)
        {
            tmpsb.Append(" Hypertrophic cardiomyopathy.");
        }
        if (ChkFamHXofCMP.Checked)
        {
            tmpsb.Append(" Familial cardiomyopathy with sudden death.");
        }
        if (ChkSuspectedRVDysplasia.Checked)
        {
            tmpsb.Append(" RV dysplasia.");
        }
        if (Chkrestrictive.Checked)
        {
            tmpsb.Append(" Peripartum cardiomyopathy.");
        }
        if (Chkmyocarditis.Checked)
        {
            tmpsb.Append(" Myocarditis.");
        }
        if (chkMI.Checked || ChkIschemic.Checked ||chkLAD.Checked|| ChkLCX.Checked || ChkRCA.Checked || intTestTotalCCS>100 || intNumVessel>0)
        {
            tmpsb.Append(" Coronary atheroscerotic heart diseas,");

            if (ChkDES.Checked)
            {
                tmpsb2.Append(" S/P percutanous revascularization with drug eluting stent");
            }
            if (ChkBMS.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" S/P percutanous revascularization with bare metal stent");
            }
            if (ChkCABG.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" S/P CABG");
            }
            if ((ChkCABG.Checked==false && ChkDES.Checked==false && ChkBMS.Checked==false) && chkPCI.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" S/P percutanous revascularization");
            }
            if (ChkCABG.Checked&& (chkautoartery.Checked && chkautovein.Checked==false))
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" with autologous arterial bypass graft");
            }
            if (ChkCABG.Checked && (chkautovein.Checked && chkautoartery.Checked==false))
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" with autologous venous bypass graft");
            }
            if (ChkCABG.Checked && (chkautovein.Checked && chkautoartery.Checked) )
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" with autologous arterial and venous bypass graft");
            }
            if (!string.IsNullOrEmpty(Txtprocedure.Text.Trim()))
            {
                if (intprocedure>0)
                {
                    if (tmpsb2.Length > 0)
                    {
                        tmpsb2.Append(",");
                    }
                    tmpsb2.AppendFormat(" last procedure {0} weeks ago", Txtprocedure.Text.Trim());
                }
            }
            if (ChkCABG.Checked==false && chkPCI.Checked==false)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" no prior revascularization procedure");
            }

            if (tmpsb2.Length > 0)
            {
                tmpsb.Append("<br /><b>History of present illness:</b><br />");
                tmpsb.Append(tmpsb2.ToString());
                tmpsb.Append(".");
                tmpsb2.Remove(0, tmpsb2.Length);
            }
        }

        if (Chkvalvular.Checked)
        {
            if (chkRMVD.Checked  )
            {
                tmpsb2.Append(" Rheumatic mitral valve disease");
            }
            if (Chkcage.Checked )
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" S/P cageball prosthetic valve");
            }
            if (chkmech.Checked )
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" S/P mechanical mitral valve replacement");
            }
            if (chkMdav.Checked )
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" S/P mechanical aortic valve replacement");
            }
            if (chkbiop.Checked )
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" S/P bioprosthetic valve replacement");
            }

            tmpsb.Append(" Valvular heart disease");
            if (tmpsb2.Length > 0)
            {
                tmpsb.Append(":");
                tmpsb.Append(tmpsb2.ToString());
                tmpsb.Append(".");
                tmpsb2.Remove(0, tmpsb2.Length);
            }
        }

        if (chkMVP.Checked)
        {
            tmpsb.Append(" Mitral valve prolapse.");
        }
        if (chkArterialIschemic.Checked)
        {
            tmpsb.Append(" Arterial ischemic stroke");
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b>Past medical history:</b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }

        if (chkSmoke.Checked)
        {
            tmpsb.Append(" History of current or recent tobacco use is present.");
        }
        if (chkSmoke.Checked==false)
        {
            tmpsb.Append(" No history of current or recent tobacco use.");
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b>Social History:</b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }

        if (chkFamily.Checked)
        {
            tmpsb.Append(" Family history of ischemic heart disease, defined as 1st degree female relative with the onset of CHD at age less than 65, 1st degree female relative with the onset of CHD at age less than 55 is present.");
        }
        else
        {
            tmpsb.Append(" No family history of ischemic heart disease is present, defined as 1st degree female relative with the onset of CHD at age less than 65, 1st degree female relative with the onset of CHD at age less than 55 is present.");
        }
        if (chkfamilyhistoryAA.Checked)
        {
            tmpsb.Append(" Family history of AAA is present.");
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b> Family history:</b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }

        if (chkweightgain.Checked)
        {
            tmpsb.Append(" Patient reports weight gain");
        }
        else
        {
            tmpsb.Append(" Patient denies weight gain");
        }
        if (chkThyro.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" reports active thyrotoxicosis");
        }
        if (chkThyro.Checked==false)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" denies active thyrotoxicosis");
        }
        if (chkHypoThyro.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" reports cold intolerance, constipation");
        }
        if (chkHypoThyro.Checked == false)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" denies cold intolerance, hypoactive thyroid");
        }
        if (Chkcough.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" reports cough");
        }
        if (chkThyro.Checked == false)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" denies cough");
        }
        if (Chksputum.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" reports sputum");
        }
        if (Chksputum.Checked == false)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" denies spum");
        }
        if (Chksinus.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" reports sinusitis/post nasal drip");
        }
        if (Chksinus.Checked == false)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" denies sinusitis or post nasal drip");
        }
        if (chkNCP.Checked == false && chkPA.Checked==false && chkTA.Checked==false)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" denies chest pain");
        }
        if (ChkDOE.Checked == false && Chkdysp.Checked==false)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" denies dyspnea");
        }
        if (ChkTestSyncope.Checked == false )
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" denies syncope");
        }
        if (Chkorthopnea.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" reports orthopnea");
        }
        else
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" denies orthopnea");
        }
        if (ChkPND.Checked == false)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" denies paroxysmal nocturnal dyspnea");
        }
        else
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" reports paroxysmal nocturnal dyspnea");
        }
        if (Chkpalpitations.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" reports palpitations");
        }
        else
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" denies palpitations");
        }
        if (ChkOSA.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" reports symptoms of OSA including observed snoring, apnea, fatigue");
        }
        else
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" denies symptoms of OSA");
        }
        if (Chkhemoptysis.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" reports hemoptysis");
        }
        else
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" denies hemoptysis");
        }
        if (ChkDVTE.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" history of pulmonary embolism");
        }
        else
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" no history of pulmonary embolism");
        }
        if (ChkPUD.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" reports active gastroduodenal ulcer");
        }
        else
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" denies active gastroduodenal ulcer");
        }
        if (Chkliver.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" reports history of liver disease");
        }
        else
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" denies history of liver disease");
        }
        if (Chkbleed.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" reports internal bleed in the past 3 months");
        }
        else
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" denies internal bleed in the past 3 months");
        }
        if (ChkTIA.Checked )
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" reports TIA like symptoms");
            if (!string.IsNullOrEmpty(Txtprocedure.Text.Trim()))
            {
                tmpsb2.AppendFormat(" occuring {0} month(s) ago", TxtPrevious.Text.Trim());
            }
        }
        else
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" denies TIA like symptoms");
        }
        if (Chkclaudication.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" reports claudication");
        }
        else
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" denies claudication");
        }
        if (ChkPVTE.Checked || ChkDVTE.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" history of previous venous thrombotic event is present");
        }
        if (ChkPVTE.Checked==false)
        {
            if (tmpsb3.Length > 0)
            {
                tmpsb3.Append(",");
            }
            tmpsb3.Append(" there is no prior history of venous thrombotic event");
        }
        if (Chkunilateral.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" admits to unilateral lower limb pain, swelling");
        }
        else
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" denies unilateral lower limb pain or swelling");
        }
        if (chkulcer.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" admits to lower limb ulceration");
        }
        else
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" denies lower limb ulceration");
        }
        if (Chkrheum.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" admits to rheumatic disease");
        }
        else
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" denies rheumatic disease");
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b> Review of Systems:</b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Append(".");
            tmpsb.Remove(0, tmpsb.Length);
        }


        if (!string.IsNullOrEmpty(txtDiffInSBP.Text.Trim()))
        {
            tmpsb.AppendFormat("Difference in SBP: {0}.", txtDiffInSBP.Text.Trim());
        }
        if (Chkhepato.Checked)
        {
            tmpsb.Append(" Neck veins unassessable.");
        }
        if (ChkJVD.Checked)
        {
            tmpsb.Append(" Abnormal jugular venous distention.");
        }
        if (ChkJVD.Checked==false && Chkhepato.Checked==false)
        {
            tmpsb.Append(" Normal neck veins.");
        }
        if (Chkbruit.Checked)
        {
            tmpsb.Append(" Carotid bruit.");
        }
        else
        {
            tmpsb.Append(" No carotid bruit.");
        }
        if ((Chkatrialfib.Checked || ChkchronicAF.Checked || Chkparoxysmal.Checked || Chkpersistent.Checked))
        {
            tmpsb.Append(" Heart sounds are irregularly irregular.");
        }
        if (Chkflutter.Checked)
        {
            tmpsb.Append(" Heart sounds are regularly irregular.");
        }
        if (Chkflutter.Checked==false && ChkchronicAF.Checked==false && Chkparoxysmal.Checked==false && Chkpersistent.Checked==false)
        {
            tmpsb.Append(" Heart sounds are rhtymic and regular.");
        }
        if (ChkS3.Checked)
        {
            tmpsb.Append(" Left sided S3 gallop is present.");
        }
        if (ChkS4.Checked && ChkchronicAF.Checked==false)
        {
            tmpsb.Append(" Left sided S4 gallop is present.");
        }
        if (Chkdistant.Checked)
        {
            tmpsb.Append(" Heart sounds are distant.");
        }
        if (chkrub.Checked)
        {
            tmpsb.Append(" There is friction rub.");
        }
        if (blnHeartMurmur)
        {
            if (blnS1MitralLoud)
            {
                tmpsb.Append(" Mitral S1 is loud.");
            }
            if (blnS1MitralNormal)
            {
                tmpsb.Append(" Mitral S1 is normointense.");
            }
            if (blnS1MitralSoft)
            {
                tmpsb.Append(" Mitral S1 is soft.");
            }
            if (blnS2AorticLoud)
            {
                tmpsb.Append(" Aortic S2 is accenuated.");
            }
            if (blnS2AorticNormal)
            {
                tmpsb.Append(" Aortic S2 is normal.");
            }
            if (blnS2AorticSoft)
            {
                tmpsb.Append(" Aortic S2 is soft.");
            }
            if (blnS1TricuspidLoud)
            {
                tmpsb.Append(" Tricuspid S1 is loud.");
            }
            if (blnS1TricuspidNormal)
            {
                tmpsb.Append(" Tricuspid S1 is normointense.");
            }
            if (blnS1TricuspidSoft)
            {
                tmpsb.Append(" Tricuspid S1 is soft.");
            }
            if (blnP2PulmonicLoud)
            {
                tmpsb.Append(" Pulmonic P2 is accenuated.");
            }
            if (blnP2PulmonicNormal)
            {
                tmpsb.Append(" Pulmonic P2 is normal.");
            }
            if (blnP2PulmonicSoft)
            {
                tmpsb.Append(" Pulmonic P2 is soft.");
            }
            if (blnsystolicclick)
            {
                tmpsb.Append(" Systolic click is present.");
            }
            if (blnejectionsound)
            {
                tmpsb.Append(" Systolic ejection sound is present.");
            }
            if (blnCresDecres)
            {
                tmpsb.Append(" Crescendo decrescendo systolic ejection murmur is noted.");
            }
            if (blnEarlyMidSysPeak)
            {
                tmpsb.Append(" The murmur peaks at early to mid systole.");
            }
            if (blnLateSysPeak)
            {
                tmpsb.Append(" The murmur peaks at early to mid systole.");
            }
            if (blnsquat && blnCresDecres)
            {
                tmpsb.Append(" Systolic ejection murmur is softer with squat.");
            }
            if (blnPlateuShaped)
            {
                tmpsb.Append(" Plateu shaped systolic murmur is noted4.");
            }
            if (blnPansystolic)
            {
                tmpsb.Append(" Murmur is pansystolic.");
            }
            if (blnMidsystolic)
            {
                tmpsb.Append(" Murmur is midsystolic.");
            }
            if (blnHalosystolic)
            {
                tmpsb.Append(" Murmur is halosystolic .");
            }
            if (blnDescrescendo)
            {
                tmpsb.Append(" Decrescendo diastolic murmur is noted.");
            }
            else if (blnRumble)
            {
                tmpsb.Append(" There is diastolic rumble.");
            }
            if (blnMitralOpenSnap)
            {
                tmpsb.Append(" There is mitral opening snap.");
            }
        }
        if (ChkPMI.Checked)
        {
            tmpsb.Append(" PMI is displaced.");
        }
        else
        {
            tmpsb.Append(" PMI is felt at the usual site.");
        }
        if (chkthorax.Checked)
        {
            tmpsb.Append(" Thorax AP diameter is increased.");
        }
        else
        {
            tmpsb.Append(" Thorax AP diameter is normal.");
        }
        if (ChkPEd.Checked)
        {
            tmpsb.Append(" Diffuse moist and dry ralles prominent in lung bases are audible.");
        }
        if (ChkMR.Checked)
        {
            tmpsb.Append(" New inspratory moist ralles are audible.");
        }
        if (chkrhonchi.Checked)
        {
            tmpsb.Append(" Rhonchi, dry ralles are audible.");
        }
        else
        {
            tmpsb.Append(" No dry or moist rales are audible.");
        }
        if (chkbreath.Checked)
        {
            tmpsb.Append(" Breath sounds are diminished.");
        }
        else
        {
            tmpsb.Append(" Normal and equal breath sounds in both lung fields.");
        }
        if (Chkascites.Checked)
        {
            tmpsb.Append(" There is ascites.");
        }
        else
        {
            tmpsb.Append(" There is no ascites.");
        }
        if (chkdistention.Checked)
        {
            tmpsb.Append(" There is abdominal distention.");
        }
        else
        {
            tmpsb.Append(" There is no abdominal distention.");
        }
        if (chkbowelsounds.Checked)
        {
            tmpsb.Append(" Bowel sounds are abnormal.");
        }
        else
        {
            tmpsb.Append(" Bowel sounds are normal.");
        }
        if (chkabdominal.Checked)
        {
            tmpsb.Append(" There is abdominal tenderness.");
        }
        else if (chkabdominal.Checked == false && chkrebound.Checked==false)
        {
            tmpsb.Append(" Abdomen is soft.");
        }
        if (chkrebound.Checked)
        {
            tmpsb.Append(" There is rebound abdominal tenderness.");
        }
        if (chkpulsatile.Checked)
        {
            tmpsb.Append(" There is pulsatile abdominal mass.");
        }
        if (chkpsaos.Checked)
        {
            tmpsb.Append(" There is psaos sign.");
        }
        if (chkMcburney.Checked)
        {
            tmpsb.Append(" There is Mcburney sign.");
        }
        if (chkRovsing.Checked)
        {
            tmpsb.Append(" There is rovsing sign.");
        }
        if (chkMurphy.Checked)
        {
            tmpsb.Append(" There is Murphy sign.");
        }
        if (chkobturator.Checked)
        {
            tmpsb.Append(" There is obturator sign.");
        }
        if (chkcarnett.Checked)
        {
            tmpsb.Append(" There is carnett sign.");
        }
        if (chkcostovertebral.Checked)
        {
            tmpsb.Append(" There is costovertebral tenderness.");
        }
        else
        {
            tmpsb.Append(" There is no costovertebral tenderness.");
        }
        if (ChkAMS.Checked)
        {
            tmpsb.Append(" Neurological findings due to low perfusion are present.");
        }
        else
        {
            tmpsb.Append(" Neuro and pshyciatric exam is intact.");
        }
        if (Chkshock.Checked)
        {
            tmpsb.Append(" Extremities are cold and clammy.");
        }
        else
        {
            tmpsb.Append(" Skin is warm to touch.");
        }
        if (chkicterus.Checked)
        {
            tmpsb.Append(" Icterus is present.");
        }
        else
        {
            tmpsb.Append(" There is no icterus.");
        }
        if (chkechymosis.Checked)
        {
            tmpsb.Append(" Abdominal wall is present.");
        }
        if (chkicterus.Checked)
        {
            tmpsb.Append(" Icterus is present.");
        }
        if (chkherpes.Checked)
        {
            tmpsb.Append(" Herpetic lesions are present.");
        }
        if (chkhenoch.Checked)
        {
            tmpsb.Append(" Purpura is present.");
        }
        if (chkcaput.Checked)
        {
            tmpsb.Append(" Caput medusa is present.");
        }
        if (Chkedema.Checked)
        {
            tmpsb.Append(" Peripheral edema present.");
        }
        else
        {
            tmpsb.Append(" There is no peripheral edema.");
        }
        if (Chkswelling.Checked)
        {
            tmpsb3.Append(" Painful deep venous palpation and edema.");
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b>Physical exam findings:</b><br />");
            if (!string.IsNullOrEmpty(txtHR.Text.Trim()))
            {
                ins.AppendFormat(" Heart Rate:{0}", txtHR.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtSBP.Text.Trim()))
            {
                ins.AppendFormat(" SBP:{0}", txtSBP.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtDBP.Text.Trim()))
            {
                ins.AppendFormat(" DBP:{0}.", txtDBP.Text.Trim());
            }
            ins.Append(tmpsb.ToString());

            if (!string.IsNullOrEmpty(txtO2sat.Text.Trim()))
            {
                tmpsb.AppendFormat(" O2sat%: {0}", txtO2sat.Text.Trim());
            }
            tmpsb.Remove(0, tmpsb.Length);
        }

        //Laboratories
        if (!string.IsNullOrEmpty(txtNameq.Text.Trim()))
        {
            tmpsb.AppendFormat("Na: {0} meq/l", txtNameq.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtHomo.Text.Trim()))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.AppendFormat("K: {0} meq/l", txtHomo.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtcreatinine.Text.Trim()))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.AppendFormat("Creatinine: {0} mg/dl", txtcreatinine.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtGlucose.Text.Trim()))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.AppendFormat("Fasting Glucose: {0}", txtGlucose.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtGFR.Text.Trim()))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.AppendFormat("GFR: {0} ml/min/1.73 M2", txtGFR.Text.Trim());
        }
        if (ChkWG.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.Append("Worsening renal function");
        }
        if (intNameq < 130)
        {
            if (!string.IsNullOrEmpty(txturineNa.Text.Trim()))
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(", ");
                }
                tmpsb.AppendFormat("Urine Na: {0} meq/l", txturineNa.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtsosm.Text.Trim()))
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(", ");
                }
                tmpsb.AppendFormat("Serum osm", txtsosm.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtuosm.Text.Trim()))
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(", ");
                }
                tmpsb.AppendFormat("Urine osm", txtuosm.Text.Trim());
            }
        }

        if (!string.IsNullOrEmpty(txtCholesterol.Text.Trim()))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.AppendFormat("Total cholesterol: {0}", txtCholesterol.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtTRG.Text.Trim()))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.AppendFormat("Triglyceride: {0}", txtTRG.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtLDL.Text.Trim()))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.AppendFormat("LDL-C: {0}", txtLDL.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtHDL.Text.Trim()))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.AppendFormat("HDL-C: {0}", txtHDL.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtApoB.Text.Trim()))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.AppendFormat("Apo B: {0}", txtApoB.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtLDLP.Text.Trim()))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.AppendFormat("LDL-P: {0}", txtLDLP.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtLPA.Text.Trim()))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.AppendFormat("LPa: {0} mg/dl", txtLPA.Text.Trim());
        }
        if (chkalready.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.Append("Already on Statin");
        }
        if (!string.IsNullOrEmpty(txtASCVD.Text.Trim()))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.AppendFormat("10 Year ASCVD Risk: {0}", txtASCVD.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtHBA1C.Text.Trim()))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.AppendFormat("HBA1C: {0}", txtHBA1C.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtCRP.Text.Trim()))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.AppendFormat("CRP: {0} mg/l", txtCRP.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtMICRO.Text.Trim()))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.AppendFormat("Albuminuria: {0} Mg/Gm or Mg/24 Hr", txtMICRO.Text.Trim());
        }
        if (chkpositivetrop.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.Append(" Positive trop");
            if (Chktrop2.Checked)
            {
                tmpsb.Append(", trop x2 normal");
            }
            if (Chktrop3.Checked)
            {
                tmpsb.Append(", trop x3 normal");
            }
        }
        if (!string.IsNullOrEmpty(txtproBNP.Text.Trim()))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.AppendFormat("NT-proBNP: {0} pg/ml", txtproBNP.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtBNP.Text.Trim()))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.AppendFormat("BNP: {0} pg/ml", txtBNP.Text.Trim());
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b>Laboratories:</b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }

        //Diagnostics
        if (chkEKG.Checked)
        {
            tmpsb.Append("EKG:");
            if (ChkNSR.Checked)
            {
                tmpsb.Append(" Normal sinus rhtym");
            }
            if (ChkPSVT.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" SVT");
            }
            if (Chkatrialfib.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" Atrial fibrillation");
            }
            if (Chkflutter.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" Atrial flutter");
            }
            if (Chkfirstdegree.Checked && (Chkatrialfib.Checked==false && Chkflutter.Checked==false ))
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb2.Append(" first degree AV block");
            }
            if (Chksecondegree.Checked && (Chkatrialfib.Checked == false && Chkflutter.Checked == false))
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" second degree AV block");
            }
            if (Chkthirdegree.Checked && (Chkatrialfib.Checked == false && Chkflutter.Checked == false))
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" third degree AV block");
            }
            if (!string.IsNullOrEmpty(txtQRS.Text.Trim()))
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.AppendFormat(" QRS Duration: {0}msec ", txtQRS.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtQRSAx.Text.Trim()))
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.AppendFormat(" QRS axis {0}degrees", txtQRSAx.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtQTc.Text.Trim()))
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.AppendFormat(" QTc {0}msec ", txtQTc.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtPRduration.Text.Trim()))
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.AppendFormat(" PR Duration: {0}msec ", txtQRS.Text.Trim());
            }
            if (ChkST.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" maximum ST deviation in any lead is >= 0.5mm");
            }
            if (ChkTWI.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" T wave inversion in any lead is > 1mm");
            }
            if (Chknonspesific.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" nonspesific ST abnormality");
            }
            if (Chklowv.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" low Voltage QRS");
            }
            if (ChkFreqVentPac.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" frequent Ventricular Ectopy");
            }
            if (ChkWPW.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" preexcitation");
            }
            if (Chkabnormq.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" abnormal Q waves");
            }
            if (Chkinfreqvent.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" infrequent Ventricular Ectopy");
            }
            if (ChkLBBB.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" LBBB");
            }
            if (ChkLVH.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" LVH");
            }
            if (ChkVPace.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" ventricular paced rhtym");
            }
            if (intQRS<120 && intPRduration<220  && ChkLVH.Checked==false && ChkST.Checked==false && ChkTWI.Checked==false && ChkLBBB.Checked==false && Chkinfreqvent.Checked==false&& Chkabnormq.Checked==false && Chkatrialfib.Checked==false && ChkWPW.Checked==false && ChkFreqVentPac.Checked==false && Chklowv.Checked==false && Chknonspesific.Checked==false && ChkVPace.Checked==false)
            {
                if (ChkNSR.Checked ) {
                    if (tmpsb.Length > 0)
                    {
                        tmpsb.Append(",");
                    }
                    tmpsb.Append(" Normal ECG");
                }  
            }
            tmpsb.Append(".");
        }

        if (chkDTS.Checked)
        {
            tmpsb.Append(" Stress Testing: ");
            if (!string.IsNullOrEmpty(TxtTTS.Text.Trim()))
            {
                tmpsb2.AppendFormat(" Duke treadmill score: {0}", TxtTTS.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtStressSummedScore.Text.Trim()))
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.AppendFormat(" Stress Summed Score: {0}", txtStressSummedScore.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtExTimeMin.Text.Trim()))
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.AppendFormat(" Exercise Time: {0} min", txtExTimeMin.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtMaxST.Text.Trim()))
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.AppendFormat(" Max ST deviation : {0} mm", txtMaxST.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtDSE.Text.Trim()))
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.AppendFormat(" Dobutamine stress echocardiogram: {0}", txtDSE.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtreversible.Text.Trim()))
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.AppendFormat(" Reversible: {0}", txtreversible.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtWMA.Text.Trim()))
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.AppendFormat(" Wall motion abnormality: {0}", txtWMA.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtMPS.Text.Trim()))
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.AppendFormat(" Myocardial perfusion scintigraphy: {0}", txtMPS.Text.Trim());
            }
            if (rbAnginaIndex.SelectedIndex > -1)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                if (rbAnginaIndex.SelectedValue == "0")
                {
                    tmpsb2.Append(" No angina during exercise");
                }
                else if (rbAnginaIndex.SelectedValue == "1")
                {
                    tmpsb2.Append(" Non-Limiting Angina");
                }
                else {
                    tmpsb2.Append(" Exercie Limited Angina");
                }
            }
            if (ChkSTelevation.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" ST elevation");
            }
            if (ChkabnormalBP.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(", ");
                }
                tmpsb2.Append(" Abnormal BP response to exercise");
            }
            if (ChkExVT.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Ventricular tachycardia");
            }
            if (ChkprolongedST.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Prolonged ST deviation");
            }
            if (chkTID.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Transient ischemic dilatation");
            }
            if (Chkuninterpret.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Uninterpretable images");
            }
            if (Chkfixed.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Fixed");
                if (Chkviable.Checked)
                {
                    tmpsb2.Append(" Viable");
                }
            }

            if (tmpsb2.Length > 0)
            {
                tmpsb.Append(tmpsb2.ToString());
                tmpsb.Append(".");
                tmpsb2.Remove(0, tmpsb2.Length);
            }
        }

        if (chkEcho.Checked)
        {
            tmpsb.Append(" Echocardiography shows:");
            if (chkpericardial.Checked)
            {
                tmpsb2.Append(" pericardial effusion");
            }
            if (chkRWMA.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" regional wall motion abnormality");
            }
            if (chkEnlargedLA.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" enlarged left atrium");
            }
            if (chkLVeccenticity.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" E/A < 1 and DT > 280ms");
            }
            if (chkMPI.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" grade 2 or higher diastolic dysfunction");
            }
            if (ChkIVSshift.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" respratory interventricular septum shift");
            }
            if (ChkmitralE.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" mitral E Variation");
            }
            if (Chkhepatic.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" hepatic Vein expratory diastolic reversal");
            }
            if (ChkEnlargedRA.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" enlarged right atrium");
            }
            if (ChkEnlargedRV.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" enlarged right ventricle");
            }
            if (Chkmidsystolicn.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" midsystolic notch");
            }
            if (!string.IsNullOrEmpty(txtMP.Text.Trim()))
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.AppendFormat(" LV lateral annulus E/E': {0}", txtMP.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtTRJ.Text.Trim()))
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.AppendFormat(" TRJ velocity m/sec: {0}", txtTRJ.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtEF.Text.Trim()))
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.AppendFormat(" LV EF: {0}", txtEF.Text.Trim());
            }

            if (tmpsb2.Length > 0)
            {
                tmpsb.Append(" ");
                tmpsb.Append(tmpsb2.ToString());
                tmpsb.Append(".");
                tmpsb2.Remove(0, tmpsb2.Length);
            }
        }

        if (chkchestxray.Checked)
        {
            tmpsb.Append(" Chest X-Ray:");
            if (chkRPLE.Checked)
            {
                tmpsb2.Append(" Right pleural effusion");
            }
            if (chkLPLE.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Left pleural effusion");
            }
            if (chkpuledema.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Pulmonary edema");
            }
            if (chkenlarcar.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Enlarged cardiac sillhouette");
            }
            if (chkWidened.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Widened Mediasteneum");
            }
            if (chkpneumothorax.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Pneumothorax");
            }
            if (chkinfiltrate.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Infiltrate");
            }
            if (chkconsolidation.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Consolidation");
            }
            if (tmpsb2.Length > 0)
            {
                tmpsb.Append(tmpsb2.ToString());
                tmpsb.Append(".");
                tmpsb2.Remove(0, tmpsb2.Length);
            }
        }

        if (ChkHC.Checked)
        {
            tmpsb.Append(" RHC performed, hemodynamics reviewed");
        }
        if (!string.IsNullOrEmpty(txtCCS.Text.Trim()))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.AppendFormat(" coronary Calcium Score is within: {0} percentile ", txtCCS.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtTotalCCS.Text.Trim()))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.AppendFormat(" total score is: {0}", txtTotalCCS.Text.Trim());
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b>Diagnostics:</b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }

        if (chkSAH.Checked && (ChkNonischemicCMP.Checked || (chkEnlargedLA.Checked && (chkLVeccenticity.Checked || chkMPI.Checked))))
        {
            tmpsb.Append(" Hypertensive heart disease");
            if (chkclass2.Checked || chkclass3.Checked || Chkdysp.Checked)
            {
                tmpsb.Append(", with symptoms of heart failure.");
            }
            else
            {
                tmpsb.Append(", without symptoms of heart failure.");
            }
        }
        if (chkMI.Checked || ChkIschemic.Checked)
        {
            tmpsb.Append(" Chronic ischemic heart disease.");
        }
        if (ChkIschemicCMP.Checked)
        {
            tmpsb.Append(" Hypertrophic cardiomyopathy.");
            if (ChkLVHGreater30.Checked)
            {
                tmpsb.Append(" with LVH >= 30mm.");
            }
            else if (ChkAbnormBP.Checked)
            {
                tmpsb.Append(" with abnormal BP response to exercise.");
            }
            else if (ChkFamHistSuddenDeath.Checked)
            {
                tmpsb.Append(" with family history of sudden death.");
            }
            else if (ChkLVOT.Checked)
            {
                tmpsb.Append(" with rest or dynamic peak LVOT gradient > 50mmHg.");
            }
            else
            {
                tmpsb.Append(" with no clinical risk factors.");
            }
        }

        if (intdur<12 && intTestCHF<=40 && intTestCHF>0)
        {
            if ((ChkJVD.Checked || chkpuledema.Checked  || ChkPEd.Checked || ChkMR.Checked) )
            {
                tmpsb.Append(" Acute systolic and combined diastolic heart failure.");
            }
        }
        if ((ChknewHF.Checked||intdur<12 )&& intTestCHF > 40 )
        {
            if (( chkpuledema.Checked || ChkPEd.Checked || ChkMR.Checked || (chkEnlargedLA.Checked &&( chkLVeccenticity.Checked || chkMPI.Checked ))))
            {
                tmpsb.Append(" Acute diastolic heart failure.");
            }
        }
        if ((ChKHFPLVF.Checked||intdur>=12 )&& intTestCHF > 40)
        {
            if ((ChkJVD.Checked || chkpuledema.Checked || ChkPEd.Checked || ChkMR.Checked))
            {
                tmpsb.Append(" Acute on chronic diastolic heart failure.");
            }
            if ((ChkJVD.Checked == false && chkpuledema.Checked==false && ChkPEd.Checked == false && ChkMR.Checked == false) && (chkEnlargedLA.Checked && (chkLVeccenticity.Checked || chkMPI.Checked)))
            {
                tmpsb.Append(" Chronic diastolic heart failure.");
            }
        }
        if (intdur>=12 && intTestCHF <= 40 && intTestCHF > 0)
        {
            if ((ChkJVD.Checked || chkpuledema.Checked || ChkPEd.Checked || ChkMR.Checked))
            {
                tmpsb.Append(" Chronic on acute systolic and combined diastolic heart failure.");
            }
            if ((ChkJVD.Checked == false  && chkpuledema.Checked == false && ChkPEd.Checked == false && ChkMR.Checked == false))
            {
                tmpsb.Append(" Chronic systolic and combined diastolic heart failure.");
            }
        }
        if (!string.IsNullOrEmpty(txtCHF.Text.Trim()))
        {
            tmpsb.AppendFormat(" LVEF: {0}", txtCHF.Text.Trim());
        }
        if (chkA.Checked)
        {
            tmpsb.Append(" Stage A HF, at high risk for HF, but without structural heart disease or symptoms of HF NYHA/CCVS CLASS 0.");
        }
        if (chkB.Checked)
        {
            tmpsb.Append(" Stage B HF, structural heart disease, but without signs/symptoms of HF.");
        }
        if (chkC.Checked)
        {
            tmpsb.Append(" Stage C HF, structural heart disease with prior or current symptoms of HF.");
        }
        if (chkD.Checked)
        {
            tmpsb.Append(" Stage D HF, refractory HF requiring specialized interventions");
            if (Chkcom.Checked)
            {
                tmpsb2.Append(" No significant comorbidities");
            }
            if (!string.IsNullOrEmpty(TxtVO2.Text.Trim()))
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.AppendFormat(" VO2 max: {0} kg", TxtVO2.Text.Trim());
            }
            if (tmpsb2.Length > 0)
            {
                tmpsb.Append(":");
                tmpsb.Append(tmpsb2.ToString());
                tmpsb.Append(".");
                tmpsb2.Remove(0, tmpsb2.Length);
            }
        }
        if (ChkNYHA1.Checked || chkclass1.Checked)
        {
            tmpsb.Append(" NYHA/CCVS CLASS 1 symptomatic");
        }
        if (ChkNYHA2.Checked || chkclass2.Checked)
        {
            tmpsb.Append(" NYHA/CCVS CLASS 2 symptomatic");
        }
        if (ChkNYHA3.Checked || chkclass3.Checked)
        {
            tmpsb.Append(" NYHA/CCVS CLASS 3 symptomatic");
        }
        if (ChkNYHA4.Checked)
        {
            tmpsb.Append(" NYHA/CCVS CLASS 4 symptomatic");
        }
        if ((ChkJVD.Checked || Chkascites.Checked || Chkedema.Checked || ChkPEd.Checked || ChkMR.Checked )&& (ChkNYHA1.Checked || ChkNYHA2.Checked || ChkNYHA3.Checked || ChkNYHA4.Checked ))
        {
            tmpsb.Append(" volume overloaded.");
        }
        if ((ChkJVD.Checked==false && Chkascites.Checked == false && Chkedema.Checked == false && ChkPEd.Checked == false && ChkMR.Checked == false ) && (ChkNYHA1.Checked || ChkNYHA2.Checked || ChkNYHA3.Checked || ChkNYHA4.Checked))
        {
            tmpsb.Append(" euvolemic.");
        }
        if (ChkIVAA.Checked)
        {
            tmpsb.Append(" The patient is on IV Antiarryhtmic");
            if (ChkAAcon.Checked)
            {
                tmpsb2.Append(" continuous");
            }
            if (ChkAAbolus.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" bolus");
            }
            if (ChkAAtitr.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" on titration");
            }
            if (!string.IsNullOrEmpty(TxtAAmonitor.Text.Trim()))
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.AppendFormat(" monitored Q : {0} hr.", TxtAAmonitor.Text.Trim());
            }
            if (ChkPOAA.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Transition to PO antiarryhtmic is planned. ");
            }
            if (tmpsb2.Length > 0)
            {
                tmpsb.Append(" (");
                tmpsb.Append(tmpsb2.ToString());
                tmpsb.Append(")");
                tmpsb2.Remove(0, tmpsb2.Length);
            }
        }
        if (Chkurgcv.Checked)
        {
            tmpsb.Append(" Status post urgent CV.");
        }
        if (Chkdefib.Checked)
        {
            tmpsb.Append(" Status post defibrillation.");
        }
        if (ChkIVHT.Checked)
        {
            tmpsb.Append(" The patient is on IV antihypertensive");
            if (ChkHTcon.Checked)
            {
                tmpsb2.Append(" continuous");
            }
            if (ChkHTbolus.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" with additional boluses");
            }
            if (ChkHTtitr.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" on titration");
            }
            if (!string.IsNullOrEmpty(TxtHTmonitor.Text.Trim()))
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.AppendFormat(" monitored Q: {0}2 hr", TxtHTmonitor.Text.Trim());
            }
            if (tmpsb2.Length > 0)
            {
                tmpsb.Append(" (");
                tmpsb.Append(tmpsb2.ToString());
                tmpsb.Append(") ");
                tmpsb2.Remove(0, tmpsb2.Length);
            }
        }
        if (ChkIVVA.Checked)
        {
            tmpsb.Append(" The patient is on IV Vasoactive");
            if (ChkVAcon.Checked)
            {
                tmpsb2.Append(" continuous");
            }
            if (ChkIVNPS.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" IV NPS");
            }
            if (ChkIVNTG.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(", ");
                }
                tmpsb2.Append(" IV NTG");
            }
            if (Chkmilrinone.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" IV milrinone");
            }
            if (ChkVAbolus.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(", ");
                }
                tmpsb2.Append(" additional IV bolus given");
            }
            if (ChkVAtitr.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" IV vasoactive is titrated as needed");
            }
            if (!string.IsNullOrEmpty(TxtVAmonitor.Text.Trim()))
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.AppendFormat(" Patient is monitored Q : {0}hr", TxtVAmonitor.Text.Trim());
            }
            if (tmpsb2.Length > 0)
            {
                tmpsb.Append(" (");
                tmpsb.Append(tmpsb2.ToString());
                tmpsb.Append(") ");
                tmpsb2.Remove(0, tmpsb2.Length);
            }
        }
        if (ChkIVDI.Checked)
        {
            tmpsb.Append(" IV diuretic");
            if (Chkcon.Checked)
            {
                tmpsb2.Append("continuous drip");
            }
            if (!string.IsNullOrEmpty(Txtmonitor.Text.Trim()))
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.AppendFormat(" Patient is monitored Q : {0} hr", Txtmonitor.Text.Trim());
            }
            if (ChkPODI.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Transition to PO diuretic");
            }
            if (tmpsb2.Length > 0)
            {
                tmpsb.Append(" (");
                tmpsb.Append(tmpsb2.ToString());
                tmpsb.Append(") ");
                tmpsb2.Remove(0, tmpsb2.Length);
            }
        }
        if (ChkNIPPV.Checked)
        {
            tmpsb.Append(" Mechanical Ventilation or NIPPV");
            if (!string.IsNullOrEmpty(txtRI.Text.Trim()))
            {
                tmpsb.AppendFormat(" (Respratory Interventions Q hr: {0})", txtRI.Text.Trim());
            }
        }
        if (!string.IsNullOrEmpty(txtOsupp.Text.Trim()))
        {
            tmpsb.AppendFormat(" O2 supplement%: {0}.", txtOsupp.Text.Trim());
        }
        if (chkult.Checked)
        {
            tmpsb.Append(" Ultrafiltration.");
        }
        if (ChkIABP.Checked)
        {
            tmpsb.Append(" IABP.");
        }
        if (ChkVAD.Checked)
        {
            tmpsb.Append(" VAD.");
        }
        if (ChkTPM.Checked)
        {
            tmpsb.Append("Temporary PM");
        }

        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b>Heart Failure</b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }

        //Coronary Heart Disease
        if (chkACS.Checked)
        {
            tmpsb.Append("ACS ");
            if (Chkang24.Checked)
            {
                tmpsb2.Append(" >=2 angina episodes in 24hrs");
            }
            if (ChkAn20.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" angina lasting > 20min");
            }
            if (ChkASA.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" ASA use in past 1 Week");
            }
            if (Chktrop.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Positive Trop I or T");
            }
            if (ChkAnAn.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" >=2 Antanginal failed");
            }
            if (chkchangetrop.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Changing Trop I or T");
            }
            if (chkrefractory.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" refractory angina ");
            }
            if (chkSTEMI.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append("STEMI");
            }
            if (!string.IsNullOrEmpty(TxtSTEMIdur.Text.Trim()))
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.AppendFormat(" STEMI duration: {0} hours", TxtSTEMIdur.Text.Trim());
            }
            if (tmpsb2.Length > 0)
            {
                tmpsb.Append(":");
                tmpsb.Append(tmpsb2.ToString());
                tmpsb.Append(" ");
                tmpsb2.Remove(0, tmpsb2.Length);
            }
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b>Unstable Coronary Syndrome :</b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }

        if (chkPCI.Checked )
        {
            if (ChkBMS.Checked)
            {
                tmpsb.Append(" S/P PCI status, BMS.");
            }
            if (ChkDES.Checked)
            {
                tmpsb.Append(" S/P PCI status, DES.");
            }
            if (ChkCABG.Checked)
            {
                tmpsb.Append(" S/P CABG surgery status. ");
            }
            if (!string.IsNullOrEmpty(Txtprocedure.Text.Trim()))
            {
                tmpsb.AppendFormat(" Last procedure {0} week(s) ago.", Txtprocedure.Text.Trim());
            }
        }
        if (chkSTENUSIS.Checked && chkACS.Checked==false)
        {
            tmpsb.Append(" STABLE ANGINA");
            if (ChkNYHA1_2.Checked)
            {
                tmpsb2.Append(" NYHA/CCVS CLASS 1");
            }
            if (ChkNYHA2_2.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" NYHA/CCVS CLASS 2");
            }
            if (ChkNYHA3_2.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" NYHA/CCVS CLASS 3");
            }
            if (ChkNYHA4_2.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" NYHA/CCVS CLASS 4");
            }
            if (tmpsb2.Length > 0)
            {
                tmpsb.Append(":");
                tmpsb.Append(tmpsb2.ToString());
                tmpsb.Append(".");
                tmpsb2.Remove(0, tmpsb2.Length);
            }
        }
        if ((chkLAD.Checked || ChkRCA.Checked || ChkLCX.Checked )&& chkACS.Checked==false&& chkSTENUSIS.Checked==false&& chkPA.Checked==false&& chkTA.Checked==false)
        {
            tmpsb.Append(" Atherosclerotic heart disease of native coronary artery without angina pectoris.");
        }
        if (chkCTO.Checked)
        {
            tmpsb.Append(" Chronic total occlusion is present.");
        }
        if (chkLM.Checked)
        {
            tmpsb.Append(" Atherosclerotic disease of left main coronary artery is present.");
        }
        if (!string.IsNullOrEmpty(txtNumVessel.Text.Trim()))
        {
            tmpsb.AppendFormat(" {0} vessel CAD with greater than > 50% lumen stenosis.", txtNumVessel.Text.Trim());
        } 
        if (!string.IsNullOrEmpty(TxtHF.Text.Trim()))
        {
            tmpsb.AppendFormat(" LVEF: {0}%.", TxtHF.Text.Trim());
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b>Coronary Atherosclerotic Heart Disease:</b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }

        if (chkPA.Checked && blnTestCHD==false)
        {
            ins.Append("<br /><b> Probable angina </b><br />");
            ins.Append(" Chest pain consistent with probable angina");
        }
        if (chkTA.Checked && blnTestCHD == false)
        {
            ins.Append("<br /><b> Typical angina </b><br />");
            ins.Append(" Chest pain, typical angina.");
        }
        if (chkNCP.Checked && blnTestCHD == false && blnPrecordial)
        {
            tmpsb.Append(" Precordial chest pain.");
        }
        if (chkNCP.Checked && blnTestCHD == false && blnpleur)
        {
            tmpsb.Append(" Pleuritic chest pain ");
        }
        if (chkNCP.Checked && blnTestCHD == false && blnCPbreath)
        {
            tmpsb.Append(" Clear intercostal chest wall pain is present.");
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b> Chest pain</b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }

        //Atrial Fibrillation
        if (!string.IsNullOrEmpty(Txtafd.Text.Trim()))
        {
            tmpsb.AppendFormat(" Atrial fibrillation / flutter duration : {0} day(s).", Txtafd.Text.Trim());
        }
        if (ChkVHD.Checked)
        {
            tmpsb.Append(" The patient is symptomatic due to atrial fibrillation.");
            if (Chkacutely.Checked)
            {
                tmpsb.Append(" (The symptoms are felt to be acute.)");
            }
        }
        if (!string.IsNullOrEmpty(TxtCMP.Text.Trim()))
        {
            tmpsb.AppendFormat(" Heart Rate with activity is: {0} beat/min.", TxtCMP.Text.Trim());
        }
        if (ChkCV.Checked)
        {
            tmpsb.Append(" Rhtym control strategy is taken.");
        }
        if (Chkpharm.Checked)
        {
            tmpsb.Append(" Ventricular rate  control strategy is taken.");
        }
        if (chkCAA.Checked)
        {
            tmpsb.Append(" The patient is currently on antiarrythmic drug therapy requiring monitoring.");
        }
        if (chkCAA.Checked)
        {
            tmpsb.Append(" Current antiarrthymic is");
            if (chkclass1AA.Checked)
            {
                tmpsb2.Append(" class 1C agent");
            }
            if (chkclass3AA.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Class 3 or 4 agent");
            }
            if (tmpsb2.Length > 0)
            {
                tmpsb.Append(tmpsb2.ToString());
                tmpsb.Append(".");
                tmpsb2.Remove(0, tmpsb2.Length);
            }
        }
        if (ChkCAC.Checked)
        {
            tmpsb.Append(" The patient is currently anticoagulated on direct thrombin inhibitor requiring monitoring.");
        }
        if (chkFactorX.Checked)
        {
            tmpsb.Append(" The patient is currently anticoagulated on factorX inhibitor requiring monitoring.");
        }
        if (ChkVKA.Checked)
        {
            tmpsb.Append(" The patient is currently anticoagulated on VKA (vitamin K antagonist) requiring monitoring.");
        }
        if (Chkparoxysmal.Checked)
        {
            tmpsb2.Append(" Paroxysmal");
        }
        if (Chkpersistent.Checked)
        {
            if (tmpsb2.Length > 0)
            {
                tmpsb2.Append(",");
            }
            tmpsb2.Append(" Persistent");
        }
        if (ChkchronicAF.Checked)
        {
            if (tmpsb2.Length > 0)
            {
                tmpsb2.Append(",");
            }
            tmpsb2.Append(" Chronic");
        }
        if ((ChkCAC.Checked || ChkVKA.Checked || chkFactorX.Checked) )
        {
            if (tmpsb2.Length > 0)
            {
                tmpsb2.Append(" atrial fibrillation, Long term use of anticoagulants");
            }
            else
            {
                tmpsb2.Append(" Long term use of anticoagulants");
            }
        }
        else
        {
            if (tmpsb2.Length > 0)
            {
                tmpsb2.Append(" atrial fibrillation");
            }
        }
        if (tmpsb2.Length > 0)
        {
            ins.Append("<br /><b>");
            ins.Append(tmpsb2.ToString());
            ins.Append(":</b><br />");
            tmpsb2.Remove(0, tmpsb2.Length);
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }

        //Thromboembolic/CVA
        if (ChkVKA.Checked)
        {
            if (Chkbleed.Checked==false)
            {
                tmpsb.Append(" Current VKA therapy, no toxicity");
            }
            else
            {
                tmpsb.Append(" Current VKA therapy");
            }
        }
        if (!string.IsNullOrEmpty(txtINR.Text.Trim()))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.AppendFormat(" INR: {0}", txtINR.Text.Trim());
            if ( dblINR>1.7 && dblINR <3.6)
            {
                tmpsb.AppendFormat(" INR is therapeutic", txtINR.Text.Trim());
            }
            if (dblINR < 1.8 )
            {
                tmpsb.AppendFormat(" INR is subtherapeutic", txtINR.Text.Trim());
            }
            if (dblINR >3.5)
            {
                tmpsb.AppendFormat(" INR is supratherapeutic", txtINR.Text.Trim());
            } 
        }
        if (chkacutecva.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Acute CVA");
            if (!string.IsNullOrEmpty(txtNIHSS.Text.Trim()))
            {
                tmpsb.AppendFormat(" NIHSS: {0}", txtNIHSS.Text.Trim());
            }
            if (CheckBox1.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" Unilateral weakness");
            }
            if (CheckBox2.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" Speech disturbance");
            }
            if (!string.IsNullOrEmpty(ssdur.Text.Trim()))
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.AppendFormat(" Duration of stroke symptoms: {0}", ssdur.Text.Trim());
            }
            if (chkpreh.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(", ");
                }
                tmpsb.Append("Pre Hospital Care");
            }
        }
        if (chkDeepVenousThrombosis.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Deep Venous Thrombosis, RLE");
        }
        if (chkDeepVenous.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Deep Venous Thrombosis, LLE ");
        }
        if (chkPE.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Pulmonary Embolism");
            if (blnsaddle)
            {
                tmpsb2.Append(" saddle");
            }
            if (blnseptic)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" septic");
            }
            if (blnotherPE)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" other");
            }
            if (ChkEnlargedRV.Checked ||ChkJVD.Checked )
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" with acute cor pulmonale");
            }
            else
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(", ");
                }
                tmpsb2.Append(" without acute cor pulmonale");
            }
            if (tmpsb2.Length > 0)
            {
                tmpsb.Append(" ");
                tmpsb.Append(tmpsb2.ToString());
                tmpsb.Append(".");
                tmpsb2.Remove(0, tmpsb2.Length);
            }
        }
           
        if (chkDVTprevention.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" DVT profilaxis, diagnosis;");
            if (Chkgeneral.Checked)
            {
                tmpsb2.Append(" Status post majorsurgery/trauma");
            }
            if (Chkurology.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Status post major gyn/urology procedure");
            }
            if (Chkknee.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Status post total knee replacement");
            }
            if (Chkhip.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Status post total hip replacement");
            }
            if (ChkHPS.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Status post hip fracture");
            }
            if (Chkcancer.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Status post spine surgery");
            }
            if (Chkspinal.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Status post spinal cord injury");
            }
            if (Chkacute.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" DVT risk factors, medical illness: ");
                if (Chkactivecancer.Checked)
                {
                    tmpsb3.Append(" Active Cancer");
                }
                if (ChkRF.Checked)
                {
                    if (tmpsb3.Length > 0)
                    {
                        tmpsb3.Append(",");
                    }
                    tmpsb3.Append(" Respiratory Failure");
                }
                if (Chkthrombo.Checked)
                {
                    if (tmpsb3.Length > 0)
                    {
                        tmpsb3.Append(",");
                    }
                    tmpsb3.Append(" Already known thrombophilic condition");
                }
                if (ChkRecent.Checked)
                {
                    if (tmpsb3.Length > 0)
                    {
                        tmpsb3.Append(",");
                    }
                    tmpsb3.Append(" Recent (<=1mo) trauma and/or surgery");
                }
                if (ChkReduced.Checked)
                {
                    if (tmpsb3.Length > 0)
                    {
                        tmpsb3.Append(",");
                    }
                    tmpsb3.Append(" Reduced mobility");
                }
                if (Chkhormone.Checked)
                {
                    if (tmpsb3.Length > 0)
                    {
                        tmpsb3.Append(",");
                    }
                    tmpsb3.Append(" Ongoing hormonal treatment");
                }
                if (Chkinfection.Checked)
                {
                    if (tmpsb3.Length > 0)
                    {
                        tmpsb3.Append(",");
                    }
                    tmpsb3.Append(" Acute infection and/or rheumatologic disorder");
                }
                if (Chkinfection.Checked==false && Chkinfection.Checked==false&& Chkhormone.Checked==false&& ChkReduced.Checked==false && ChkRecent.Checked==false&& Chkthrombo.Checked==false &&ChkRF.Checked==false && Chkactivecancer.Checked==false)
                {
                    if (tmpsb3.Length > 0)
                    {
                        tmpsb3.Append(",");
                    }
                    tmpsb3.Append(" No clinical risk factors for DVT");
                }
                if (chkbldrisk.Checked)
                {
                    if (tmpsb3.Length > 0)
                    {
                        tmpsb3.Append(",");
                    }
                    if (Chkplt.Checked)
                    {
                        tmpsb4.Append(" Platelet Count < 50 × 109/L");
                    }
                    if (ChkICU.Checked)
                    {
                        if (tmpsb4.Length > 0)
                        {
                            tmpsb4.Append(",");
                        }
                        tmpsb4.Append(" ICU or CCU admission");
                    }
                    if (ChkCVC.Checked)
                    {
                        if (tmpsb4.Length > 0)
                        {
                            tmpsb4.Append(",");
                        }
                        tmpsb4.Append(" Central venous catheter");
                    }
                    if (tmpsb4.Length > 0)
                    {
                        tmpsb3.Append(" ");
                        tmpsb3.Append(" is (are) bleeding risk factor(s).");
                        tmpsb3.Append(tmpsb4.ToString());
                        tmpsb4.Remove(0, tmpsb4.Length);
                    }
                }
                if (tmpsb3.Length > 0)
                {
                    tmpsb2.Append(" ");
                    tmpsb2.Append(tmpsb3.ToString());
                    tmpsb3.Remove(0, tmpsb3.Length);
                }
            }
            if (tmpsb2.Length > 0)
            {
                tmpsb.Append(tmpsb2.ToString());
                tmpsb2.Remove(0, tmpsb2.Length);
            }
        }
        if (Chkcage.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.Append(" Status post cageball mechanical prosthetic valve");
        }
        if (chkmech.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Status post disk / bileaflet mechanical prosthetic mitral valve replacement");
        }
        if (chkMdav.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Status post disk / bileaflet mechanical prosthetic aortic valve replacement");
        }
        if (chkRMVD.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Rheumatic mitral valve disease");
            if (ChkLAthr.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" LA Thrombus is present");
            }
            if (!string.IsNullOrEmpty(txtLAd.Text.Trim()))
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.AppendFormat(" left atrium diameter is: {0} cm", txtLAd.Text.Trim());
            }
        }
        if (chkbiop.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Status post bioprosthetic valve replacement");
        }
        if (chkIE.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Infective endocarditis");
        }
        if (chkLPVT.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Left sided PV thrombosis");
        }
        if (chkRPVT.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Right sided PV thrombosis");
        }
        if (Chksysemb.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" associated with systemic embolism");
        }
        if (chkArterialIschemic.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Nonacute arterial ischemic stroke associated with");
        }
        if (chkAorticArchAth.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" aortic arch atherosclerosis");
        }
        if (ChkLVt.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" LV thrombus");
        }
        if (chkPFO.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" PFO");
        }
        if (chkMVP.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" MVP");
        }
        if (chkMitralAnnular.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" mitral annular calcification");
        }
        if (chkCalcificAortic.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" calcific aortic valve disease");
        }
        if (chkHeparinIndThrom.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append( "HIT");
        }
        if (ChkApoA1.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Hypercoagulable state");
        }
        if (chkCerebralVenous.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Cerebral Venous Thrombosis");
        }
        if (chkminorpro.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Procedural bleed risk not clinically important or low");
        }
        if (chksurgery.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Procedural bleed risk is indetermined");
        }
        if (chkemergencysurgery.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Procedural bleed risk is indetermined or high");
        }
        if (chkprior.Checked && (chkminorpro.Checked || chksurgery.Checked || chkemergencysurgery.Checked))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" There is history of perioperative bleeding during a similar procedure");
        }
        if (chkprior.Checked==false && (chkminorpro.Checked || chksurgery.Checked || chkemergencysurgery.Checked))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" There is no history of perioperative bleeding during a similar procedure");
        }
        if (chkpriorASA.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.Append(" Prior ASA Use");
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b>Thromboembolic disease:</b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }

        //Syncope
        if (blnTestSYNCOPE)
        {
            tmpsb.Append(" Clinical Syncope / Collapse");
        }
        if (ChkSinusNodeDysfunction.Checked || ChkAVNodeDisease.Checked || ChkVasoDepressorSyncope.Checked || ChkCarotidSinusHypersensitivity.Checked || ChkPosturalOrthostatisTac.Checked
            || ChkAutonomicFailureSyndrome.Checked)
        {
            tmpsb.Append(" due to");
            if (ChkSinusNodeDysfunction.Checked)
            {
                tmpsb2.Append(" sinus Node Dysfunction");
            }
            if (ChkAVNodeDisease.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" AV Node Disease");
            }
            if (ChkVasoDepressorSyncope.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" isolated neurocardiogenic syncope");
            }
            if (ChkCarotidSinusHypersensitivity.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" carotid sinus hypersensitivity");
            }
            if (ChkPosturalOrthostatisTac.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" postural orthostatic tachycardia syndrome");
            }
            if (ChkAutonomicFailureSyndrome.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" autonomic failure syndrome");
            }
            if (tmpsb2.Length > 0)
            {
                tmpsb.Append(tmpsb2.ToString());
                tmpsb2.Remove(0, tmpsb2.Length);
            }
        }
        if(ChkHistCardArrest.Checked || ChkBrugada.Checked || ChkLongQT.Checked || Chksarc.Checked)
        {
            tmpsb.Append(" associated with ");
            if (ChkHistCardArrest.Checked)
            {
                tmpsb2.Append(" history of cardiac arrest.");
            }
            if (ChkBrugada.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" Brugada Syndrome");
            }
            if (ChkLongQT.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" LongQT/Catecholaminergic Polymorphic VT");
            }
            if (Chksarc.Checked)
            {
                if (tmpsb2.Length > 0)
                {
                    tmpsb2.Append(",");
                }
                tmpsb2.Append(" sarcoidosis/Giant Cell/Chagas");
            }
            if (tmpsb2.Length > 0)
            {
                tmpsb.Append(tmpsb2.ToString());
                tmpsb2.Remove(0, tmpsb2.Length);
            }
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b>Syncope/Bradyarrthymias:</b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }

        //Ventricular Tachyarrthymias
        if (ChkNSVT.Checked)
        {
            tmpsb.Append("NSVT");
        }
        if (chkMVT.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Monomorphic VT");
        }
        if (chkRMVT.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Repetetive Monomorphic VT");
        }
        if (chkPVT.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Polymorphic VT ");
        }
        if (chkTors.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Torsades");
        }
        if (chkIVT.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Incessant VT");
        }
        if (chkidioVT.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Idiopathic VT");
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b>Ventricular Tachyarrthymias:</b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }

        //Symptomatic Carotid Disease
        if (chkNoncardioCVA.Checked)
        {
            tmpsb.Append(" Noncardioembolic CVA assocaiated with > 50% carotid stenosis");
        }
        if (chkNoncardioTIA.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.Append(" Noncardioembolic TIA assocaiated with > 50% carotid stenosis");
        }
        if (chkCEA.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(", ");
            }
            tmpsb.Append(" >= 70% Carotid Stenosis / S/P CEA");
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b>Symptomatic Carotid Disease:</b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }

        //Peripheric Vascular Disease
        if (chkABI.Checked)
        {
            tmpsb.Append("ABI < 0.9");
        }
        if (Chkcritical.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Critical Limb Ischemia");
        }
        if (Chkbypass.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" S/P Aortofemoral Byspass");
        }
        if (ChkBKbypass.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" S/P Below Knee Byspass");
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b>Peripheric Vascular Disease:</b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }

        //Abdominal Aortic Aneurysm
        if (chkAADiameter.Checked)
        {
            ins.Append("<br /><b>Abdominal Aortic Aneurysm:</b><br />");
            ins.Append(" AA Diameter > 5cm");
        }
        if (chkthoracic.Checked)
        {
            ins.Append("<br /><b>Thoracic Aortic Aneurysm:</b><br />");
            ins.Append(" Thoracic aortic aneurysm, unruptured");
        }
        if (chkthoracoabdominal.Checked)
        {
            ins.Append("<br /><b>Thoracoabdominal Aortic Aneurysm:</b><br />");
            ins.Append(" Thoracoabdominal aortic aneurysm, unruptured");
        }

        //Diabetes
        if (chkDM.Checked || chkDM1.Checked)
        { 
            if (chkDM.Checked)
            {
                tmpsb.Append(" Type II DM");
            }
            if (chkDM1.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" Type I DM");
            }
            if (chkgestational.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" Gestational diabetes");
            }
            if (chkDMangio.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" peripheral angiopathy without gangrene");
            }
            if (chkDMgangrene.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" peripheral angiopathy with gangrene");
            }
            if (chkDMothercirc.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" other circulatory complication");
            }
            if (chkDMNP.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" diabetic nephropathy");
            }
            if (chkDMCKD.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" diabetic CKD");
            }
            if (chkDMother.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" other renal complications than diabetic nephropathy");
            }
            if (chkDMmono.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" diabetic mononeuropathy");
            }
            if (chkDMpoly.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" diabetic polyneuropathy");
            }
            if (chkDMautonom.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" diabetic autonom neuropathy");
            }
            if (chkDMarthro.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" diabetic arthropathy");
            }
            if (chkDMskin.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" diabetic skin complications");
            }
            if (chkDMoral.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" diabetic oral complications");
            }
            if (chkDMhypo.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(" ");
                }
                tmpsb.Append(" hypoglycemia without coma ");
            }
            if (chkDMhypocoma.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" hypoglycemia with coma");
            }
            if (chkDMhyper.Checked || (intTestGlucose>120 && intTestGlucose<600))
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(", ");
                }
                tmpsb.Append(" hyperglycemia");
            }
            if (intTestGlucose>600 && (ChkAMS.Checked=false))
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" hyperosmolality without coma");
            }
            if (intTestGlucose > 600 && (ChkAMS.Checked ))
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" hyperosmolality with coma");
            }
            if (chkDMunspec.Checked)
            {
                if (tmpsb.Length > 0)
                {
                    tmpsb.Append(",");
                }
                tmpsb.Append(" with unspecified complications");
            }
        }
        if (ChkRetinopathy.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Retinopathy");
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b>Diabetes:</b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }

        //Major Cardiovascular Risk Factors
        if (chkSAH.Checked)
        {
            tmpsb.Append(" Systemic Arterial Hypertension");
            if (Val_int(txtAge.Text)>65)
            {
                if (intTestSBP>149 || intTestDBP>89)
                {
                    tmpsb2.Append(" uncontrolled");
                }
                else
                {
                    tmpsb2.Append(" controlled");
                }
            }
            if (Val_int(txtAge.Text) < 66)
            {
                if (intTestSBP > 139 || intTestDBP > 89)
                {
                    tmpsb2.Append(" uncontrolled");
                }
                else
                {
                    tmpsb2.Append(" controlled");
                }
            }
            tmpsb.Append(tmpsb2.ToString());
            tmpsb2.Remove(0, tmpsb2.Length);
            if ((intTestGFR < 45 && intTestGFR > 0) || Chkaldo.Checked || Chkrenovas.Checked || ChkFMD.Checked || ChkOSA.Checked)
            {
                tmpsb2.Append(" secondary");
                if (intTestGFR < 45 && intTestGFR > 0)
                {
                    if (tmpsb2.Length > 0)
                    {
                        tmpsb2.Append(",");
                    }
                    tmpsb2.Append(" renal");
                }
                if (Chkaldo.Checked)
                {
                    if (tmpsb2.Length > 0)
                    {
                        tmpsb2.Append(",");
                    }
                    tmpsb2.Append(" primary hyperaldesteronism");
                }
                if (Chkrenovas.Checked)
                {
                    if (tmpsb2.Length > 0)
                    {
                        tmpsb2.Append(",");
                    }
                    tmpsb2.Append(" renovascular, atherosclerotic");
                }
                if (ChkFMD.Checked)
                {
                    if (tmpsb2.Length > 0)
                    {
                        tmpsb2.Append(",");
                    }
                    tmpsb2.Append(" fibromuscular dysplasia");
                }
                if (ChkOSA.Checked)
                {
                    if (tmpsb2.Length > 0)
                    {
                        tmpsb2.Append(",");
                    }
                    tmpsb2.Append(" obstructive sleep apnea");
                }
            }
            if (Chkhead.Checked || Chkepis.Checked || ChkCP.Checked)
            {
                tmpsb2.Append(" Acutely symptomatic hypertensive crisis");
                if (Chkhead.Checked)
                {
                    if (tmpsb2.Length > 0)
                    {
                        tmpsb2.Append(",");
                    }
                    tmpsb2.Append(" headache");
                }
                if (Chkepis.Checked)
                {
                    if (tmpsb2.Length > 0)
                    {
                        tmpsb2.Append(",");
                    }
                    tmpsb2.Append(" epistaxis");
                }
                if (ChkCP.Checked)
                {
                    if (tmpsb2.Length > 0)
                    {
                        tmpsb2.Append(",");
                    }
                    tmpsb2.Append(" chest or back pain or dyspnea");
                }
            }
            if (tmpsb2.Length > 0)
            {
                tmpsb.Append(tmpsb2.ToString());
                tmpsb2.Remove(0, tmpsb2.Length);
            }
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b>Major Cardiovascular Risk Factors:</b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }

        if (!string.IsNullOrEmpty(TxtMETS.Text.Trim()))
        {
            tmpsb.AppendFormat(" METS: {0}", TxtMETS.Text.Trim());
        }
        if (!string.IsNullOrEmpty(TxtDASI.Text.Trim()))
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.AppendFormat(" DASI: {0}", TxtDASI.Text.Trim());
        }
        if (Chkunab.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Unable to Exercise");
        }
        if (Chkelective.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Elective Surgery");
        }
        if (Chkemergency.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Emergency Surgery");
        }
        if (Chkvascular.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Vascular Surgery");
        }
        if (Chklowriskelec.Checked)
        {
            if (tmpsb.Length > 0)
            {
                tmpsb.Append(",");
            }
            tmpsb.Append(" Low Risk Surgery");
        }
        if (tmpsb.Length > 0)
        {
            ins.Append("<br /><b></b><br />");
            ins.Append(tmpsb.ToString());
            tmpsb.Remove(0, tmpsb.Length);
        }

        Session["InputSummary"] = ins.ToString();

        //Response.Write(strRet)
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.open('Outputs.aspx','_blank','height=600,width=1000,left=100,top=30,resizable=Yes,scrollbars=Yes,toolbar=no,menubar=no,location=no,directories=no, status=No');</script>)");
        ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());

        //Response.Write("<script>window.open('Outputs.aspx','_blank','height=500,width=900,left=100,top=30,resizable=Yes,scrollbars=Yes,toolbar=no,menubar=no,location=no,directories=no, status=No');</script>")
    }

    public string Left(string str, int length)
    {
        return str.Substring(0, Math.Min(str.Length, length));
    }
   

    protected void Save_CalcVal()
    {
        dataWork dw = new dataWork(dbNames.OLEDB);
        dw.ConnectionString = CVDevaluator.SharedFunctions.GetConnStr("ConnString3");
        dataCommand dcmd = new dataCommand();
        dcmd.commandExecuteType = commandExecuteType.INSERTORUPDATE;

        StringBuilder sb1 = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        sb1.Append("INSERT INTO Patients(PageType,User_ID,FullName,Age,Gender,SBP,DBP,Rest) VALUES(0,@USER0_ID,@NAME,@AGE,@GENDER,@SBP,@DBP,@REST)");

        dcmd.Parameters.Add(new Param("@USER0_ID", Session["USER0_ID"], dbTypes.CHAR));
        dcmd.Parameters.Add(new Param("@NAME", Left(txtFirstName.Text.Trim(), 50), dbTypes.CHAR));
        dcmd.Parameters.Add(new Param("@AGE", Left(txtAge.Text, 2), dbTypes.CHAR));
        dcmd.Parameters.Add(new Param("@GENDER", (!string.IsNullOrEmpty(rblGender.Text) ? rblGender.Text : "1"), dbTypes.CHAR));
        dcmd.Parameters.Add(new Param("@SBP", txtSBP.Text, dbTypes.CHAR));
        dcmd.Parameters.Add(new Param("@DBP", txtDBP.Text, dbTypes.CHAR));

        if (!string.IsNullOrEmpty(txtWeight.Text.Trim()))
        {
            sb2.AppendFormat("weight={0}|", txtWeight.Text);
        }
        if (!string.IsNullOrEmpty(txtWaist.Text.Trim()))
        {
            sb2.AppendFormat("waist={0}|", txtWaist.Text);
        }
        if (!string.IsNullOrEmpty(txtBMI.Text.Trim()))
        {
            sb2.AppendFormat("txtBMI={0}|", txtBMI.Text);
        }
        if (chkAsianCode.Checked)
        {
            sb2.Append("chkAsianCode|");
        }
        if (!string.IsNullOrEmpty(txtOrthostaticSBP.Text.Trim()))
        {
            sb2.AppendFormat("OrthostaticSBP={0}|", txtOrthostaticSBP.Text);
        }
        if (chkSymptomatic.Checked)
        {
            sb2.Append("chkSymptomatic|");
        }
        if (!string.IsNullOrEmpty(txtHR.Text.Trim()))
        {
            sb2.AppendFormat("HR={0}|", txtHR.Text);
        }
        if (!string.IsNullOrEmpty(txtASBP.Text.Trim()))
        {
            sb2.AppendFormat("ASBP={0}|", txtASBP.Text);
        }
        if (!string.IsNullOrEmpty(txtADBP.Text.Trim()))
        {
            sb2.AppendFormat("ADBP={0}|", txtADBP.Text);
        }
        if (chkpreg.Checked)
        {
            sb2.Append("chkpreg|");
        }
        if (ChKHFPLVF.Checked)
        {
            sb2.Append("chkHFPLVF|");
        }
        if (Chklung.Checked)
        {
            sb2.Append("chklung|");
        }
        if (ChknewHF.Checked)
        {
            sb2.Append("chknewHF|");
        }
        if (!string.IsNullOrEmpty(txtdur.Text.Trim()))
        {
            sb2.AppendFormat("txtdur={0}|", txtdur.Text);
        }
        if (ChkD1.Checked)
        {
            sb2.Append("chkD1|");
        }
        if (ChkD2.Checked)
        {
            sb2.Append("chkD2|");
        }
        if (ChkD3.Checked)
        {
            sb2.Append("chkD3|");
        }
        if (ChkD4.Checked)
        {
            sb2.Append("chkD4|");
        }
        if (ChkD5.Checked)
        {
            sb2.Append("chkD5|");
        }
        if (Chkfoutpt.Checked)
        {
            sb2.Append("chkfoutpt|");
        }
        if (chkICD.Checked)
        {
            sb2.Append("chkICD|");
        }
        if (ChkDOE.Checked)
        {
            sb2.Append("chkDOE|");
            if (chkclass1.Checked)
            {
                sb2.Append("chkclass1|");
            }
            if (chkclass2.Checked)
            {
                sb2.Append("chkclass2|");
            }
            if (chkclass3.Checked)
            {
                sb2.Append("chkclass3|");
            }
        }
        if (Chkdysp.Checked)
        {
            sb2.Append("chkdysp|");
        }
        if (ChkWeight.Checked)
        {
            sb2.Append("chkWeight|");
        }
        if (ChkPEd.Checked)
        {
            sb2.Append("chkPEd|");
        }
        if (ChkS3.Checked)
        {
            sb2.Append("chkS3|");
        }
        if (ChkPMI.Checked)
        {
            sb2.Append("chkPMI|");
        }
        if (ChkS4.Checked)
        {
            sb2.Append("chkS4|");
        }
        if (Chkbruit.Checked)
        {
            sb2.Append("chkbruit|");
        }
        if (ChkHJR.Checked)
        {
            sb2.Append("chkHJR|");
        }
        if (Chkdistant.Checked)
        {
            sb2.Append("chkdistant|");
        }
        if (Chkascites.Checked)
        {
            sb2.Append("chkascites|");
        }
        if (ChkMR.Checked)
        {
            sb2.Append("chkMR|");
        }
        if (Chkhepato.Checked)
        {
            sb2.Append("chkhepato|");
        }
        if (ChkJVD.Checked)
        {
            sb2.Append("chkJVD|");
        }
        if (Chkedema.Checked)
        {
            sb2.Append("chkedema|");
        }
        if (ChkAMS.Checked)
        {
            sb2.Append("chkAMS|");
        }
        if (ChkWG.Checked)
        {
            sb2.Append("chkWG|");
        }
        if (Chkshock.Checked)
        {
            sb2.Append("chkshock|");
        }
        if (!string.IsNullOrEmpty(txtCHF.Text.Trim()))
        {
            sb2.AppendFormat("txtCHF={0}|", txtCHF.Text);
        }
        if (!string.IsNullOrEmpty(txtO2sat.Text.Trim()))
        {
            sb2.AppendFormat("txtO2sat={0}|", txtO2sat.Text);
        }
        if (ChkNonischemicCMP.Checked)
        {
            sb2.Append("chkNonischemicCMP|");
        }
        if (chkMI.Checked)
        {
            sb2.Append("chkMI|");
            if (chkLVaneurysm.Checked)
            {
                sb2.Append("chkLVaneurysm|");
            }
        }
        if (ChkIschemic.Checked)
        {
            sb2.Append("chkIschemic|");
        }
        if (ChkCardiotoxins.Checked)
        {
            sb2.Append("chkCardiotoxins|");
        }
        if (ChkFamHXofCMP.Checked)
        {
            sb2.Append("chkFamHXofCMP|");
        }
        if (Chkmyocarditis.Checked)
        {
            sb2.Append("chkmyocarditis|");
        }
        if (ChkSuspectedRVDysplasia.Checked)
        {
            sb2.Append("chkSuspectedRVDysplasia|");
        }
        if (ChkIschemicCMP.Checked)
        {
            sb2.Append("chkIschemicCMP|");
            if (ChkLVHGreater30.Checked)
            {
                sb2.Append("chkLVHGreater30|");
            }
            if (ChkAbnormBP.Checked)
            {
                sb2.Append("chkAbnormBP|");
            }
            if (ChkFamHistSuddenDeath.Checked)
            {
                sb2.Append("chkFamHistSuddenDeath|");
            }
            if (ChkLVOT.Checked)
            {
                sb2.Append("chkLVOT|");
            }
        }
        if (Chkrestrictive.Checked)
        {
            sb2.Append("chkrestrictive|");
        }
        if (chkasthma.Checked)
        {
            sb2.Append("chkasthma|");
        }
        if (chkILD.Checked)
        {
            sb2.Append("chkILD|");
        }
        if (chkCOPD.Checked)
        {
            sb2.Append("chkCOPD|");
        }
        if (!string.IsNullOrEmpty(txtFEV1.Text.Trim()))
        {
            sb2.AppendFormat("txtFEV1={0}|", txtFEV1.Text);
        }
        if (!string.IsNullOrEmpty(txtFEVP.Text.Trim()))
        {
            sb2.AppendFormat("txtFEVP={0}|", txtFEVP.Text);
        }
        if (!string.IsNullOrEmpty(txtFVCP.Text.Trim()))
        {
            sb2.AppendFormat("txtFVCP={0}|", txtFVCP.Text);
        }
        if (!string.IsNullOrEmpty(txtDLCO.Text.Trim()))
        {
            sb2.AppendFormat("txtDLCO={0}|", txtDLCO.Text);
        }
        if (chkA.Checked)
        {
            sb2.Append("chkA|");
        }
        if (chkB.Checked)
        {
            sb2.Append("chkB|");
        }
        if (chkC.Checked)
        {
            sb2.Append("chkC|");
        }
        if (chkD.Checked)
        {
            sb2.Append("chkD|");
            if (Chkcom.Checked)
            {
                sb2.Append("chkcom|");
            }
            if (!string.IsNullOrEmpty(TxtVO2.Text.Trim()))
            {
                sb2.AppendFormat("TxtVO2={0}|", TxtVO2.Text);
            }
        }
        if (ChkNYHA1.Checked)
        {
            sb2.Append("chkNYHA1|");
        }
        else if (chkSTENUSIS.Checked)
        {
            if (ChkNYHA1_2.Checked)
            {
                sb2.Append("chkNYHA1|");
            }
        }
        if (ChkNYHA2.Checked)
        {
            sb2.Append("chkNYHA2|");
        }
        else if (chkSTENUSIS.Checked)
        {
            if (ChkNYHA2_2.Checked)
            {
                sb2.Append("chkNYHA2|");
            }
            if (ChkNYHA1_2.Checked)
            {
                sb2.Append("chkNYHA1|");
            }
            if (ChkNYHA3_2.Checked)
            {
                sb2.Append("chkNYHA3|");
            }
            if (ChkNYHA4_2.Checked)
            {
                sb2.Append("chkNYHA4|");
            }
        }
        if (ChkNYHA3.Checked)
        {
            sb2.Append("chkNYHA3|");
        }
        else if (chkSTENUSIS.Checked)
        {
            if (ChkNYHA3_2.Checked)
            {
                sb2.Append("chkNYHA3|");
            }
        }
        if (ChkNYHA4.Checked)
        {
            sb2.Append("chkNYHA4|");
        }
        else if (chkSTENUSIS.Checked)
        {
            if (ChkNYHA4_2.Checked)
            {
                sb2.Append("chkNYHA4|");
            }
        }
        if (ChkIVAA.Checked)
        {
            sb2.Append("chkIVAA|");
            if (ChkAAcon.Checked)
            {
                sb2.Append("chkAAcon|");
            }
            if (ChkAAbolus.Checked)
            {
                sb2.Append("chkAAbolus|");
            }
            if (ChkAAtitr.Checked)
            {
                sb2.Append("chkAAtitr|");
            }
            if (!string.IsNullOrEmpty(TxtAAmonitor.Text.Trim()))
            {
                sb2.AppendFormat("TxtAAmonitor={0}|", TxtAAmonitor.Text);
            }
            if (ChkPOAA.Checked)
            {
                sb2.Append("chkPOAA|");
            }
        }
        if (Chkurgcv.Checked)
        {
            sb2.Append("chkurgcv|");
        }
        if (Chkdefib.Checked)
        {
            sb2.Append("chkdefib|");
        }
        if (ChkIVHT.Checked)
        {
            sb2.Append("chkIVHT|");
            if (ChkHTcon.Checked)
            {
                sb2.Append("chkHTcon|");
            }
            if (ChkHTbolus.Checked)
            {
                sb2.Append("chkHTbolus|");
            }
            if (ChkHTtitr.Checked)
            {
                sb2.Append("chkHTtitr|");
            }
            if (!string.IsNullOrEmpty(TxtHTmonitor.Text.Trim()))
            {
                sb2.AppendFormat("TxtHTmonitor={0}|", TxtHTmonitor.Text);
            }
        }
        if (ChkIVVA.Checked)
        {
            sb2.Append("chkIVVA|");
            if (ChkVAcon.Checked)
            {
                sb2.Append("chkVAcon|");
            }
            if (ChkVAbolus.Checked)
            {
                sb2.Append("chkVAbolus|");
            }
            if (ChkVAtitr.Checked)
            {
                sb2.Append("chkVAtitr|");
            }
            if (!string.IsNullOrEmpty(TxtVAmonitor.Text.Trim()))
            {
                sb2.AppendFormat("TxtVAmonitor={0}|", TxtVAmonitor.Text);
            }
            if (ChkIVNPS.Checked)
            {
                sb2.Append("chkIVNPS|");
            }
            if (ChkIVNTG.Checked)
            {
                sb2.Append("chkIVNTG|");
            }
            if (Chkmilrinone.Checked)
            {
                sb2.Append("chkmilrinone|");
            }
        }
        if (ChkIVDI.Checked)
        {
            sb2.Append("chkIVDI|");
            if (Chkcon.Checked)
            {
                sb2.Append("chkcon|");
            }
            if (ChkPODI.Checked)
            {
                sb2.Append("chkPODI|");
            }
            if (!string.IsNullOrEmpty(Txtmonitor.Text.Trim()))
            {
                sb2.AppendFormat("Txtmonitor={0}|", Txtmonitor.Text);
            }
        }
        if (ChkNIPPV.Checked)
        {
            sb2.Append("chkNIPPV|");
            if (!string.IsNullOrEmpty(txtRI.Text.Trim()))
            {
                sb2.AppendFormat("txtRI={0}|", txtRI.Text);
            }
        }
        if (!string.IsNullOrEmpty(txtOsupp.Text.Trim()))
        {
            sb2.AppendFormat("txtOsupp={0}|", txtOsupp.Text);
        }
        if (ChkVAD.Checked)
        {
            sb2.Append("chkVAD|");
        }
        if (chkult.Checked)
        {
            sb2.Append("chkult|");
        }
        if (ChkIABP.Checked)
        {
            sb2.Append("chkIABP|");
        }
        if (ChkTPM.Checked)
        {
            sb2.Append("chkTPM|");
        }

        //CHD
        if (chkACS.Checked)
        {
            sb2.Append("chkACS|");
            if (Chkang24.Checked)
            {
                sb2.Append("chkang24|");
            }
            if (ChkAn20.Checked)
            {
                sb2.Append("chkAn20|");
            }
            if (ChkASA.Checked)
            {
                sb2.Append("chkASA|");
            }
            if (Chktrop.Checked)
            {
                sb2.Append("chktrop|");
            }
            if (ChkAnAn.Checked)
            {
                sb2.Append("chkAnAn|");
            }
            if (chkSTEMI.Checked)
            {
                sb2.Append("chkSTEMI|");
                if (chkanterior.Checked)
                {
                    sb2.Append("chkanterior|");
                }
                if (chkinferior.Checked)
                {
                    sb2.Append("chkinferior|");
                }
                if (chkother.Checked)
                {
                    sb2.Append("chkother|");
                }
            }
            if (chkrefractory.Checked)
            {
                sb2.Append("chkrefractory|");
            }
            if (chkchangetrop.Checked)
            {
                sb2.Append("chkchangetrop|");
            }
        }
        if (chkPCI.Checked)
        {
            sb2.Append("chkPCI|");
            if (!string.IsNullOrEmpty(Txtprocedure.Text.Trim()))
            {
                sb2.AppendFormat("Txtprocedure={0}|", Txtprocedure.Text);
            }
            if (ChkBMS.Checked)
            {
                sb2.Append("chkBMS|");
            }
            if (ChkDES.Checked)
            {
                sb2.Append("chkDES|");
            }
            if (ChkCABG.Checked)
            {
                sb2.Append("chkCABG|");
                if (chkautovein.Checked)
                {
                    sb2.Append("chkautovein|");
                }
                if (chkautoartery.Checked)
                {
                    sb2.Append("chkautoartery|");
                }
                if (chknonauto.Checked)
                {
                    sb2.Append("chknonauto|");
                }
            }
        }
        if (chkSTENUSIS.Checked)
        {
            sb2.Append("chkSTENUSIS|");
        }
        if (chkLM.Checked)
        {
            sb2.Append("chkLM|");
        }
        if (chkLAD.Checked)
        {
            sb2.Append("chkLAD|");
        }
        if (ChkRCA.Checked)
        {
            sb2.Append("chkRCA|");
        }
        if (ChkLCX.Checked)
        {
            sb2.Append("chkLCX|");
        }
        if (!string.IsNullOrEmpty(TxtHF.Text.Trim()))
        {
            sb2.AppendFormat("TxtHF={0}|", TxtHF.Text);
        }
        if (!string.IsNullOrEmpty(txtNumVessel.Text.Trim()))
        {
            sb2.AppendFormat("txtNumVessel={0}|", txtNumVessel.Text);
        }
        if (!string.IsNullOrEmpty(TxtSTEMIdur.Text.Trim()))
        {
            sb2.AppendFormat("TxtSTEMIdur={0}|", TxtSTEMIdur.Text);
        }
        if (chkCTO.Checked)
        {
            sb2.Append("chkCTO|");
        }
        if (Chkcoraneurysm.Checked)
        {
            sb2.Append("chkcoraneurysm|");
        }
        if (Chkcordissection.Checked)
        {
            sb2.Append("chkcordissection|");
        }
        if (Chkcalcified.Checked)
        {
            sb2.Append("chkcalcified|");
        }
        if (Chklipidrich.Checked)
        {
            sb2.Append("chklipidrich|");
        }
        if (Chkvasospasm.Checked)
        {
            sb2.Append("chkvasospasm|");
        }

        //Atrial Fibrillation
        if (Chkparoxysmal.Checked)
        {
            sb2.Append("chkparoxysmal|");
        }
        if (Chkpersistent.Checked)
        {
            sb2.Append("chkpersistent|");
        }
        if (ChkchronicAF.Checked)
        {
            sb2.Append("chkchronicAF|");
        }
        if (!string.IsNullOrEmpty(Txtafd.Text.Trim()))
        {
            sb2.AppendFormat("Txtafd={0}|", Txtafd.Text);
        }
        if (Chkpharm.Checked)
        {
            sb2.Append("chkpharm|");
        }
        if (!string.IsNullOrEmpty(TxtCMP.Text.Trim()))
        {
            sb2.AppendFormat("TxtCMP={0}|", TxtCMP.Text);
        }
        if (ChkVHD.Checked)
        {
            sb2.Append("chkVHD|");
            if (Chkacutely.Checked)
            {
                sb2.Append("chkacutely|");
            }
        }
        if (ChkCV.Checked)
        {
            sb2.Append("chkCV|");
        }
        if (chkCAA.Checked)
        {
            sb2.Append("chkCAA|");
            if (chkclass1AA.Checked)
            {
                sb2.Append("chkclass1AA|");
            }
            if (chkclass3AA.Checked)
            {
                sb2.Append("chkclass3AA|");
            }
        }
        if (Chkliver.Checked)
        {
            sb2.Append("chkliver|");
        }
        if (Chkablation.Checked)
        {
            sb2.Append("chkablation|");
        }
        //if (chkPrevious.Checked)
        //{
        //    sb2.Append("chkPrevious|");
        //}
        if (chkThyro.Checked)
        {
            sb2.Append("chkThyro|");
        }
        if (chkHypoThyro.Checked)
        {
            sb2.Append("chkHypoThyro|");
        }
        if (ChkOSA.Checked)
        {
            sb2.Append("chkOSA|");
        }
        if (Chkcough.Checked)
        {
            sb2.Append("Chkcough|");
        }
        if (Chksputum.Checked)
        {
            sb2.Append("Chksputum|");
        }
        if (Chksinus.Checked)
        {
            sb2.Append("Chksinus|");
        }
        if (Chkflutter.Checked)
        {
            sb2.Append("chkflutter|");
            if (chktypicalAF.Checked)
            {
                sb2.Append("chktypicalAF|");
            }
            if (chkatypicalAF.Checked)
            {
                sb2.Append("chkatypicalAF|");
            }
        }
        if (Chksecondegree.Checked)
        {
            sb2.Append("chksecondegree|");
            if (Chktype1.Checked)
            {
                sb2.Append("chktype1|");
            }
            if (Chktype2.Checked)
            {
                sb2.Append("chktype2|");
            }
        }
        if (Chkatrialfib.Checked)
        {
            sb2.Append("chkatrialfib|");

        }
        if (Chkfirstdegree.Checked)
        {
            sb2.Append("chkfirstdegree|");

        }
        if (Chksecondegree.Checked)
        {
            sb2.Append("chksecondegree|");

        }
        if (ChkPUD.Checked)
        {
            sb2.Append("chkPUD|");
        }
        if (ChkTIA.Checked)
        {
            sb2.Append("chkTIA|");
        }
        if (Chkorthopnea.Checked)
        {
            sb2.Append("chkorthopnea|");
        }
        if (ChkPND.Checked)
        {
            sb2.Append("chkPND|");
        }
        if (Chkpalpitations.Checked)
        {
            sb2.Append("chkpalpitations|");
        }
        if (ChkDVTE.Checked)
        {
            sb2.Append("chkDVTE|");
        }
        if (Chkbleed.Checked)
        {
            sb2.Append("chkbleed|");
        }
        if (Chkrheum.Checked)
        {
            sb2.Append("chkrheum|");
        }
        //Thromboembolic/VKA
        if (chkbldrisk.Checked)
        {
            sb2.Append("chkbldrisk|");
            
            if (Chkplt.Checked)
            {
                sb2.Append("chkplt|");
            }
            if (ChkICU.Checked)
            {
                sb2.Append("chkICU|");
            }
            if (ChkCVC.Checked)
            {
                sb2.Append("chkCVC|");
            }
           
        }
        if (ChkVKA.Checked)
        {
            sb2.Append("chkVKA|");
        }
        if (!string.IsNullOrEmpty(txtINR.Text.Trim()))
        {
            sb2.AppendFormat("txtINR={0}|", txtINR.Text);
        }
        if (ChkCAC.Checked)
        {
            sb2.Append("chkCAC|");
        }
        if (chkFactorX.Checked)
        {
            sb2.Append("chkFactorX|");
        }
        if (chkDeepVenousThrombosis.Checked)
        {
            sb2.Append("chkDeepVenousThrombosis|");
        }
        if (chkDeepVenous.Checked)
        {
            sb2.Append("chkDeepVenous|");
        }
        if (chkPE.Checked)
        {
            sb2.Append("chkPE|");
            if (chksaddle.Checked)
            {
                sb2.Append("chksaddle|");
            }
            if (chkseptic.Checked)
            {
                sb2.Append("chkseptic|");
            }
            if (chkotherPE.Checked)
            {
                sb2.Append("chkotherPE|");
            }
        }
        if (chkprior.Checked)
        {
            sb2.Append("chkprior|");
        }
        if (chkpriorASA.Checked)
        {
            sb2.Append("chkpriorASA|");
        }
        if (Chkhemoptysis.Checked)
        {
            sb2.Append("chkhemoptysis|");
        }
        if (ChkPVTE.Checked)
        {
            sb2.Append("chkPVTE|");
        }
        if (Chkunilateral.Checked)
        {
            sb2.Append("chkunilateral|");
        }
        if (chkulcer.Checked)
        {
            sb2.Append("chkulcer|");
        }
        if (ChkRecent.Checked)
        {
            sb2.Append("chkRecent|");
        }
        if (chkDVTprevention.Checked)
        {
            sb2.Append("chkDVTprevention|");
            if (Chkgeneral.Checked)
            {
                sb2.Append("chkgeneral|");
            }
            if (Chkurology.Checked)
            {
                sb2.Append("chkurology|");
            }
            if (Chkknee.Checked)
            {
                sb2.Append("chkknee|");
            }
            if (Chkhip.Checked)
            {
                sb2.Append("chkhip|");
            }
            if (ChkHPS.Checked)
            {
                sb2.Append("chkHPS|");
            }
            if (Chkcancer.Checked)
            {
                sb2.Append("chkcancer|");
            }
            if (Chkspinal.Checked)
            {
                sb2.Append("chkspinal|");
            }
            if (Chkacute.Checked)
            {
                sb2.Append("chkacute|");
                if (Chkswelling.Checked)
                {
                    sb2.Append("chkswelling|");
                }
                if (Chkactivecancer.Checked)
                {
                    sb2.Append("chkactivecancer|");
                }
                
                if (ChkRF.Checked)
                {
                    sb2.Append("chkRF|");
                }
                if (Chkthrombo.Checked)
                {
                    sb2.Append("chkthrombo|");
                }
                
                if (ChkReduced.Checked)
                {
                    sb2.Append("chkReduced|");
                }
                if (Chkhormone.Checked)
                {
                    sb2.Append("chkhormone|");
                }
                if (Chkinfection.Checked)
                {
                    sb2.Append("chkinfection|");
                }
            }
        }
        if (chkacutecva.Checked)
        {
            sb2.Append("chkacutecva|");
            if (!string.IsNullOrEmpty(txtNIHSS.Text.Trim()))
            {
                sb2.AppendFormat("txtNIHSS={0}|", txtNIHSS.Text);
            }
            if (CheckBox1.Checked)
            {
                sb2.Append("chk1|");
            }
            if (CheckBox2.Checked)
            {
                sb2.Append("chk2|");
            }
            if (!string.IsNullOrEmpty(ssdur.Text.Trim()))
            {
                sb2.AppendFormat("ssdur={0}|", ssdur.Text);
            }
            if (chkpreh.Checked)
            {
                sb2.Append("chkpreh|");
            }
        }
        if (Chkcage.Checked)
        {
            sb2.Append("chkcage|");
        }
        if (chkmech.Checked)
        {
            sb2.Append("chkmech|");
        }
        if (chkMdav.Checked)
        {
            sb2.Append("chkMdav|");
        }
        if (chkRMVD.Checked)
        {
            sb2.Append("chkRMVD|");
            if (ChkLAthr.Checked)
            {
                sb2.Append("chkLAthr|");
            }
            if (!string.IsNullOrEmpty(txtLAd.Text.Trim()))
            {
                sb2.AppendFormat("txtLAd={0}|", txtLAd.Text);
            }
        }
        if (chkbiop.Checked)
        {
            sb2.Append("chkbiop|");
        }
        if (Chksysemb.Checked)
        {
            sb2.Append("chksysemb|");
        }
        if (chkIE.Checked)
        {
            sb2.Append("chkIE|");
        }
        if (chkLPVT.Checked)
        {
            sb2.Append("chkLPVT|");
        }
        if (chkRPVT.Checked)
        {
            sb2.Append("chkRPVT|");
        }
        if (chkArterialIschemic.Checked)
        {
            sb2.Append("chkArterialIschemic|");
        }
        if (chkAorticArchAth.Checked)
        {
            sb2.Append("chkAorticArchAth|");
        }
        if (ChkLVt.Checked)
        {
            sb2.Append("chkLVt|");
        }
        if (chkPFO.Checked)
        {
            sb2.Append("chkPFO|");
        }
        if (chkMVP.Checked)
        {
            sb2.Append("chkMVP|");
        }
        if (chkMitralAnnular.Checked)
        {
            sb2.Append("chkMitralAnnular|");
        }
        if (chkCalcificAortic.Checked)
        {
            sb2.Append("chkCalcificAortic|");
        }
        if (chkHeparinIndThrom.Checked)
        {
            sb2.Append("chkHeparinIndThrom|");
        }
        if (ChkApoA1.Checked)
        {
            sb2.Append("chkApoA1|");
        }
        if (chkCerebralVenous.Checked)
        {
            sb2.Append("chkCerebralVenous|");
        }
        if (chkminorpro.Checked)
        {
            sb2.Append("chkminorpro|");
        }
        if (chksurgery.Checked)
        {
            sb2.Append("chksurgery|");
        }
        if (chkemergencysurgery.Checked)
        {
            sb2.Append("chkemergencysurgery|");
        }

        //Syncope
        if (ChkTestSyncope.Checked)
        {
            sb2.Append("chkTestSyncope|");
            if (chkSynStanding.Checked)
            {
                sb2.Append("chkSynStanding|");
            }
            if (chkSynsporadic.Checked)
            {
                sb2.Append("chkSynsporadic|");
            }
            if (chkSynProSitting.Checked)
            {
                sb2.Append("chkSynProSitting|");
            }
            if (chkSynOther.Checked)
            {
                sb2.Append("chkSynOther|");
            }
            if (chkSynneck.Checked)
            {
                sb2.Append("chkSynneck|");
            }
            if (chkSynNausea.Checked)
            {
                sb2.Append("chkSynNausea|");
            }
            if (chkSyntrauma.Checked)
            {
                sb2.Append("chkSyntrauma|");
            }
            if (chkSynDizziness.Checked)
            {
                sb2.Append("chkSynDizziness|");
            }
            if (chkSynPalpitations.Checked)
            {
                sb2.Append("chkSynPalpitations|");
            }
            if (chkSynProCP.Checked)
            {
                sb2.Append("chkSynProCP|");
            }
            if (chkSynsporadic.Checked)
            {
                sb2.Append("chkSynsporadic|");
            }
            if (chkSyntrauma.Checked)
            {
                sb2.Append("chkSyntrauma|");
            }

            if (!string.IsNullOrEmpty(txtSynMin.Text.Trim()))
            {
                sb2.AppendFormat("txtSynMin={0}|", txtSynMin.Text);
            }
            if (!string.IsNullOrEmpty(txtSynSec.Text.Trim()))
            {
                sb2.AppendFormat("txtSynSec={0}|", txtSynSec.Text);
            }
            if (chkSynNone.Checked)
            {
                sb2.Append("chkSynNone|");
            }
            if (chkSynDisor.Checked)
            {
                sb2.Append("chkSynDisor|");
            }
            if (chkSynPara.Checked)
            {
                sb2.Append("chkSynPara|");
            }
            if (chkSynPosCP.Checked)
            {
                sb2.Append("chkSynPosCP|");
            }
            if (chkSynaura.Checked)
            {
                sb2.Append("chkSynaura|");
            }
            if (chkSynfatigue.Checked)
            {
                sb2.Append("chkSynfatigue|");
            }
            if (chkSynpallor.Checked)
            {
                sb2.Append("chkSynpallor|");
            }
            if (chkSynposture.Checked)
            {
                sb2.Append("chkSynposture|");
            }
        }
        if (ChkSinusNodeDysfunction.Checked)
        {
            sb2.Append("chkSinusNodeDysfunction|");
        }
        if (ChkAVNodeDisease.Checked)
        {
            sb2.Append("chkAVNodeDisease|");
        }
        if (ChkVasoDepressorSyncope.Checked)
        {
            sb2.Append("chkVasoDepressorSyncope|");
        }
        if (ChkCarotidSinusHypersensitivity.Checked)
        {
            sb2.Append("chkCarotidSinusHypersensitivity|");
        }
        if (ChkPosturalOrthostatisTac.Checked)
        {
            sb2.Append("chkPosturalOrthostatisTac|");
        }
        if (ChkAutonomicFailureSyndrome.Checked)
        {
            sb2.Append("chkAutonomicFailureSyndrome|");
        }
        if (ChkHistCardArrest.Checked)
        {
            sb2.Append("chkHistCardArrest|");
        }
        if (ChkBrugada.Checked)
        {
            sb2.Append("chkBrugada|");
        }
        if (ChkLongQT.Checked)
        {
            sb2.Append("chkLongQT|");
        }
        if (Chksarc.Checked)
        {
            sb2.Append("chksarc|");
        }

        //Ventricular Tachyarrthymias
        if (ChkNSVT.Checked)
        {
            sb2.Append("chkNSVT|");
        }
        if (chkMVT.Checked)
        {
            sb2.Append("chkMVT|");
        }
        if (chkRMVT.Checked)
        {
            sb2.Append("chkRMVT|");
        }
        if (chkPVT.Checked)
        {
            sb2.Append("chkPVT|");
        }
        if (chkTors.Checked)
        {
            sb2.Append("chkTors|");
        }
        if (chkIVT.Checked)
        {
            sb2.Append("chkIVT|");
        }
        if (chkidioVT.Checked)
        {
            sb2.Append("chkidioVT|");
        }

        //Surgical Risk
        if (Chkemergency.Checked)
        {
            sb2.Append("chkemergency|");
        }
        if (Chkelective.Checked)
        {
            sb2.Append("chkelective|");
        }
        if (Chkvascular.Checked)
        {
            sb2.Append("chkvascular|");
        }
        if (Chklowriskelec.Checked)
        {
            sb2.Append("chklowriskelec|");
        }
        if (!string.IsNullOrEmpty(TxtMETS.Text.Trim()))
        {
            sb2.AppendFormat("TxtMETS={0}|", TxtMETS.Text);
        }
        if (!string.IsNullOrEmpty(TxtDASI.Text.Trim()))
        {
            sb2.AppendFormat("TxtDASI={0}|", TxtDASI.Text);
        }
        if (Chkunab.Checked)
        {
            sb2.Append("chkunab|");
        }

        //Chest Pain
        if (chkTA.Checked)
        {
            sb2.Append("chkTA|");
            if (chkescTA.Checked)
            {
                sb2.Append("chkescTA|");
            }
        }
        if (chkPA.Checked)
        {
            sb2.Append("chkPA|");
        }
        if (chkNCP.Checked)
        {
            sb2.Append("chkNCP|");
            if (chkpleur.Checked)
            {
                sb2.Append("chkpleur|");
            }
            if (chkCPbreath.Checked)
            {
                sb2.Append("chkCPbreath|");
            }
            if (chkPrecordial.Checked)
            {
                sb2.Append("chkPrecordial|");
            }
            if (chkpleur.Checked | chkCPbreath.Checked | chkPrecordial.Checked)
            {
                if (chkCPSharp.Checked)
                {
                    sb2.Append("chkCPSharp|");
                }
                if (chkCPPressure.Checked)
                {
                    sb2.Append("chkCPPressure|");
                }
                if (chkCPTightness.Checked)
                {
                    sb2.Append("chkCPTightness|");
                }
                if (chkCPSoreness.Checked)
                {
                    sb2.Append("chkCPSoreness|");
                }
                if (Chktearing.Checked)
                {
                    sb2.Append("chktearing|");
                }
                if (chkCPLying.Checked)
                {
                    sb2.Append("chkCPLying|");
                }
                if (chkCPBreathing.Checked)
                {
                    sb2.Append("chkCPBreathing|");
                }
                if (chkCPCoughing.Checked)
                {
                    sb2.Append("chkCPCoughing|");
                }
                if (chkCPExertion.Checked)
                {
                    sb2.Append("chkCPExertion|");
                }
                if (chkCPPalpation.Checked)
                {
                    sb2.Append("chkCPPalpation|");
                }
                if (chkCPNTG.Checked)
                {
                    sb2.Append("chkCPNTG|");
                }
                if (chkCPRest.Checked)
                {
                    sb2.Append("chkCPRest|");
                }
                if (chkCPSitting.Checked)
                {
                    sb2.Append("chkCPSitting|");
                }
                if (chkCPlyingside.Checked)
                {
                    sb2.Append("chkCPlyingside|");
                }
                if (chkCPAnalgesics.Checked)
                {
                    sb2.Append("chkCPAnalgesics|");
                }
                if (chkCPJaw.Checked)
                {
                    sb2.Append("chkCPJaw|");
                }
                if (chkCPBack.Checked)
                {
                    sb2.Append("chkCPBack|");
                }
                if (chkCPArm.Checked)
                {
                    sb2.Append("chkCPArm|");
                }
                if (chkCPDyspnea.Checked)
                {
                    sb2.Append("chkCPDyspnea|");
                }
                if (chkCPNausea.Checked)
                {
                    sb2.Append("chkCPNausea|");
                }
                if (chkCPDiaph.Checked)
                {
                    sb2.Append("chkCPDiaph|");
                }
                if (chkCPFaint.Checked)
                {
                    sb2.Append("chkCPFaint|");
                }
                if (chkCPpalpitations.Checked)
                {
                    sb2.Append("chkCPpalpitations|");
                }
                if (chkCPseconds.Checked)
                {
                    sb2.Append("chkCPseconds|");
                }
                if (chkCPminutes.Checked)
                {
                    sb2.Append("chkCPminutes|");
                }
                if (chkCPhours.Checked)
                {
                    sb2.Append("chkCPhours|");
                }
                if (chkCPconstant.Checked)
                {
                    sb2.Append("chkCPconstant|");
                }
                if (chkrub.Checked)
                {
                    sb2.Append("chkrub|");
                }
                if (chkbreath.Checked)
                {
                    sb2.Append("chkbreath|");
                }
                if (chkabdominal.Checked)
                {
                    sb2.Append("chkabdominal|");
                }
                if (!string.IsNullOrEmpty(txtDiffInSBP.Text.Trim()))
                {
                    sb2.AppendFormat("txtDiffInSBP={0}|", txtDiffInSBP.Text);
                }
            }
        }
        if (chkER.Checked)
        {
            sb2.Append("chkER|");
            if (chkangina.Checked)
            {
                sb2.Append("chkangina|");
            }
            if (chkangina20.Checked)
            {
                sb2.Append("chkangina20|");
            }
            if (chkASAuse.Checked)
            {
                sb2.Append("chkASAuse|");
            }
            if (chkpositivetrop.Checked)
            {
                sb2.Append("chkpositivetrop|");
            }
        }
        if (chkunable.Checked)
        {
            sb2.Append("chkunable|");
        }

        //SCD
        if (chkNoncardioCVA.Checked)
        {
            sb2.Append("chkNoncardioCVA|");
        }
        if (chkNoncardioTIA.Checked)
        {
            sb2.Append("chkNoncardioTIA|");
        }
        if (chkCEA.Checked)
        {
            sb2.Append("chkCEA|");
        }

        //PVD
        if (chkABI.Checked)
        {
            sb2.Append("chkABI|");
        }
        if (Chkclaudication.Checked)
        {
            sb2.Append("chkclaudication|");
        }
        if (Chkcritical.Checked)
        {
            sb2.Append("chkcritical|");
        }
        if (Chkbypass.Checked)
        {
            sb2.Append("chkbypass|");
        }
        if (ChkBKbypass.Checked)
        {
            sb2.Append("chkBKbypass|");
        }

        //AAA
        if (chkAADiameter.Checked)
        {
            sb2.Append("chkAADiameter|");
        }
        if (chkthoracic.Checked)
        {
            sb2.Append("chkthoracic|");
        }
        if (chkthoracoabdominal.Checked)
        {
            sb2.Append("chkthoracoabdominal|");
        }
        //Valvular
        if (Chkvalvular.Checked)
        {
            sb2.Append("chkvalvular|");
        }
        if (chkHeartMurmur.Checked)
        {
            sb2.Append("chkHeartMurmur|");
            if (chkS1Mitral.Checked)
            {
                sb2.Append("chkS1Mitral|");
                if (chkS1MitralLoud.Checked)
                {
                    sb2.Append("chkS1MitralLoud|");
                }
                if (chkS1MitralNormal.Checked)
                {
                    sb2.Append("chkS1MitralNormal|");
                }
                if (chkS1MitralSoft.Checked)
                {
                    sb2.Append("chkS1MitralSoft|");
                }
            }
            if (chkS1Tricuspid.Checked)
            {
                sb2.Append("chkS1Tricuspid|");
                if (chkS1TricuspidLoud.Checked)
                {
                    sb2.Append("chkS1TricuspidLoud|");
                }
                if (chkS1TricuspidNormal.Checked)
                {
                    sb2.Append("chkS1TricuspidNormal|");
                }
                if (chkS1TricuspidSoft.Checked)
                {
                    sb2.Append("chkS1TricuspidSoft|");
                }
            }
            if (chkS2Aortic.Checked)
            {
                sb2.Append("chkS2Aortic|");
                if (chkS2MitralLoud.Checked)
                {
                    sb2.Append("chkS2MitralLoud|");
                }
                if (chkS2MitralNormal.Checked)
                {
                    sb2.Append("chkS2MitralNormal|");
                }
                if (chkS2MitralSoft.Checked)
                {
                    sb2.Append("chkS2MitralSoft|");
                }
            }
            if (chkP2Pulmonic.Checked)
            {
                sb2.Append("chkP2Pulmonic|");
                if (chkP2PulmonicLoud.Checked)
                {
                    sb2.Append("chkP2PulmonicLoud|");
                }
                if (chkP2PulmonicNormal.Checked)
                {
                    sb2.Append("chkP2PulmonicNormal|");
                }
                if (chkP2PulmonicSoft.Checked)
                {
                    sb2.Append("chkP2PulmonicSoft|");
                }
            }
            if (chkSystolicMurmur.Checked)
            {
                sb2.Append("chkSystolicMurmur|");
                if (chkCresDecres.Checked)
                {
                    sb2.Append("chkCresDecres|");
                    if (chkEarlyMidPeak.Checked)
                    {
                        sb2.Append("chkEarlyMidPeak|");
                    }
                    if (chkLatePeak.Checked)
                    {
                        sb2.Append("chkLatePeak|");
                    }
                }
                if (chkPlateuShaped.Checked)
                {
                    sb2.Append("chkPlateuShaped|");
                    if (chkHalosystolic.Checked)
                    {
                        sb2.Append("chkHalosystolic|");
                    }
                    if (chkPansystolic.Checked)
                    {
                        sb2.Append("chkPansystolic|");
                    }
                    if (chkMidsystolic.Checked)
                    {
                        sb2.Append("chkMidsystolic|");
                    }

                }
                if (chksquat.Checked)
                {
                    sb2.Append("chksquat|");
                    

                }
            }
            if (chkDiastolicMurmur.Checked)
            {
                sb2.Append("chkDiastolicMurmur|");
                if (chkDecrescendo.Checked)
                {
                    sb2.Append("chkDecrescendo|");
                }
                if (chkRumble.Checked)
                {
                    sb2.Append("chkRumble|");
                }
            }
            if (chkMitralOpeningSnap.Checked)
            {
                sb2.Append("chkMitralOpeningSnap|");
            }
        }

        //Diabetes
        if (chkDM.Checked | chkDM1.Checked)
        {
            //sb2.Append("chkDM|");
            if (chkDMNP.Checked)
            {
                sb2.Append("chkDMNP|");
            }
            if (chkDMCKD.Checked)
            {
                sb2.Append("chkDMCKD|");
            }
            if (chkDMother.Checked)
            {
                sb2.Append("chkDMother|");
            }
            if (chkDMmono.Checked)
            {
                sb2.Append("chkDMmono|");
            }
            if (chkDMpoly.Checked)
            {
                sb2.Append("chkDMpoly|");
            }
            if (chkDMautonom.Checked)
            {
                sb2.Append("chkDMautonom|");
            }
            if (chkDMangio.Checked)
            {
                sb2.Append("chkDMangio|");
            }
            if (chkDMgangrene.Checked)
            {
                sb2.Append("chkDMgangrene|");
            }
            if (chkDMothercirc.Checked)
            {
                sb2.Append("chkDMothercirc|");
            }
            if (chkDMarthro.Checked)
            {
                sb2.Append("chkDMarthro|");
            }
            if (chkDMskin.Checked)
            {
                sb2.Append("chkDMskin|");
            }
            if (chkDMoral.Checked)
            {
                sb2.Append("chkDMoral|");
            }
            if (chkDMhypo.Checked)
            {
                sb2.Append("chkDMhypo|");
            }
            if (chkDMhypocoma.Checked)
            {
                sb2.Append("chkDMhypocoma|");
            }
            if (chkDMhyper.Checked)
            {
                sb2.Append("chkDMhyper|");
            }
            if (chkDMothercomp.Checked)
            {
                sb2.Append("chkDMothercomp|");
            }
            if (chkDMunspec.Checked)
            {
                sb2.Append("chkDMunspec|");
            }
            if (chkDMwithout.Checked)
            {
                sb2.Append("chkDMwithout|");
            }
        }
        //if (chkDM1.Checked)
        //{
        //    sb2.Append("chkDM1|");
        //}
        if (chkgestational.Checked)
        {
            sb2.Append("chkgestational|");
        }
        if (ChkRetinopathy.Checked)
        {
            sb2.Append("chkRetinopathy|");
        }

        //Risk Factors
        if (chkSAH.Checked)
        {
            sb2.Append("chkSAH|");
            if (chkSBPTreated.Checked)
            {
                sb2.Append("chkSBPTreated|");
                

            }
            if (chkAA.Checked)
            {
                sb2.Append("chkAA|");
            }
            if (Chkaldo.Checked)
            {
                sb2.Append("chkaldo|");
            }
            if (Chkrenovas.Checked)
            {
                sb2.Append("chkrenovas|");
            }
            if (ChkFMD.Checked)
            {
                sb2.Append("chkFMD|");
            }
           
            if (Chkhead.Checked)
            {
                sb2.Append("chkhead|");
            }
            if (Chkepis.Checked)
            {
                sb2.Append("chkepis|");
            }
            if (ChkCP.Checked)
            {
                sb2.Append("chkCP|");
            }
        }
        if (chkSmoke.Checked)
        {
            sb2.Append("chkSmoke|");
        }
        if (chkFamily.Checked)
        {
            sb2.Append("chkFamily|");
        }
        if (chkCKD.Checked)
        {
            sb2.Append("chkCKD|");
        }
        //Home Meds
        if (Chkcar.Checked)
        {
            sb2.Append("chkcar|");
            if (chkBisoprolol25.Checked)
            {
                sb2.Append("chkBisoprolol25|");
            }
            if (chkCarvedilol625.Checked)
            {
                sb2.Append("chkCarvedilol625|");
            }
            if (chkCarvedilol125.Checked)
            {
                sb2.Append("chkCarvedilol125|");
            }
            if (chkCarvedilol25.Checked)
            {
                sb2.Append("chkCarvedilol25|");
            }
            if (chkBisoprolol5.Checked)
            {
                sb2.Append("chkBisoprolol5|");
            }
            if (chkMetoprololER50.Checked)
            {
                sb2.Append("chkMetoprololER50|");
            }
            if (chkMetoprololER100.Checked)
            {
                sb2.Append("chkMetoprololER100|");
            }
            if (chkBisoprolol10.Checked)
            {
                sb2.Append("chkBisoprolol10|");
            }
            if (chkMetoprololER200.Checked)
            {
                sb2.Append("chkMetoprololER200|");
            }
        }
        if (Chkace.Checked)
        {
            sb2.Append("chkace|");
            if (chklisinopril5.Checked)
            {
                sb2.Append("chklisinopril5|");
            }
            if (chklisinopril10.Checked)
            {
                sb2.Append("chklisinopril10|");
            }
            if (chklisinopril20.Checked)
            {
                sb2.Append("chklisinopril20|");
            }
            if (chklosartan25.Checked)
            {
                sb2.Append("chklosartan25|");
            }
            if (chklosartan50.Checked)
            {
                sb2.Append("chklosartan50|");
            }
            if (chklosartan100.Checked)
            {
                sb2.Append("chklosartan100|");
            }
        }
        if (ChkCCB.Checked)
        {
            sb2.Append("chkCCB|");
        }
        if (Chkvasodilator.Checked)
        {
            sb2.Append("chkvasodilator|");
        }
        if (Chkdiuretic.Checked)
        {
            sb2.Append("chkdiuretic|");
            if (chkfurosemide40.Checked)
            {
                sb2.Append("chkfurosemide40|");
            }
            if (chkfurosemide80.Checked)
            {
                sb2.Append("chkfurosemide80|");
            }
            if (chkfurosemide81.Checked)
            {
                sb2.Append("chkfurosemide81|");
            }
            if (chkbumex1.Checked)
            {
                sb2.Append("chkbumex1|");
            }
            if (chkbumex2.Checked)
            {
                sb2.Append("chkbumex2|");
            }
            if (chkbumex3.Checked)
            {
                sb2.Append("chkbumex3|");
            }
            if (chktorsemide20.Checked)
            {
                sb2.Append("chktorsemide20|");
            }
            if (chktorsemide40.Checked)
            {
                sb2.Append("chktorsemide40|");
            }
            if (chktorsemide50.Checked)
            {
                sb2.Append("chktorsemide50|");
            }
            if (ChkHCTZ.Checked)
            {
                sb2.Append("chkHCTZ|");
            }
            if (Chkindapamide.Checked)
            {
                sb2.Append("chkindapamide|");
            }
            if (Chkchlorthalidone.Checked)
            {
                sb2.Append("chkchlorthalidone|");
            }
        }

        //Laboratories
        if (!string.IsNullOrEmpty(txtNameq.Text.Trim()))
        {
            sb2.AppendFormat("txtNameq={0}|", txtNameq.Text);
        }
        if (!string.IsNullOrEmpty(txtHomo.Text.Trim()))
        {
            sb2.AppendFormat("txtHomo={0}|", txtHomo.Text);
        }
        if (!string.IsNullOrEmpty(txtcreatinine.Text.Trim()))
        {
            sb2.AppendFormat("txtcreatinine={0}|", txtcreatinine.Text);
        }
        if (!string.IsNullOrEmpty(txtBUN.Text.Trim()))
        {
            sb2.AppendFormat("txtBUN={0}|", txtBUN.Text);
        }
        if (!string.IsNullOrEmpty(txtGlucose.Text.Trim()))
        {
            sb2.AppendFormat("txtGlucose={0}|", txtGlucose.Text);
        }
        if (!string.IsNullOrEmpty(txtGFR.Text.Trim()))
        {
            sb2.AppendFormat("txtGFR={0}|", txtGFR.Text);
        }
        if (!string.IsNullOrEmpty(txturineNa.Text.Trim()))
        {
            sb2.AppendFormat("txturineNa={0}|", txturineNa.Text);
        }
        if (!string.IsNullOrEmpty(txtsosm.Text.Trim()))
        {
            sb2.AppendFormat("txtsosm={0}|", txtsosm.Text);
        }
        if (!string.IsNullOrEmpty(txtuosm.Text.Trim()))
        {
            sb2.AppendFormat("txtuosm={0}|", txtuosm.Text);
        }
        if (chkstatint.Checked)
        {
            sb2.Append("chkstatint|");
        }
        if (!string.IsNullOrEmpty(txtCholesterol.Text.Trim()))
        {
            sb2.AppendFormat("txtCholesterol={0}|", txtCholesterol.Text);
        }
        if (!string.IsNullOrEmpty(txtTRG.Text.Trim()))
        {
            sb2.AppendFormat("txtTRG={0}|", txtTRG.Text);
        }
        if (!string.IsNullOrEmpty(txtLDL.Text.Trim()))
        {
            sb2.AppendFormat("txtLDL={0}|", txtLDL.Text);
        }
        if (!string.IsNullOrEmpty(txtHDL.Text.Trim()))
        {
            sb2.AppendFormat("txtHDL={0}|", txtHDL.Text);
        }
        if (!string.IsNullOrEmpty(txtApoB.Text.Trim()))
        {
            sb2.AppendFormat("txtApoB={0}|", txtApoB.Text);
        }
        if (!string.IsNullOrEmpty(txtLDLP.Text.Trim()))
        {
            sb2.AppendFormat("txtLDLP={0}|", txtLDLP.Text);
        }
        if (!string.IsNullOrEmpty(txtLPA.Text.Trim()))
        {
            sb2.AppendFormat("txtLPA={0}|", txtLPA.Text);
        }
        if (chkalready.Checked)
        {
            sb2.Append("chkalready|");
        }
        if (!string.IsNullOrEmpty(txtASCVD.Text.Trim()))
        {
            sb2.AppendFormat("txtASCVD={0}|", txtASCVD.Text);
        }
        if (!string.IsNullOrEmpty(txtHBA1C.Text.Trim()))
        {
            sb2.AppendFormat("txtHBA1C={0}|", txtHBA1C.Text);
        }
        if (!string.IsNullOrEmpty(txtCRP.Text.Trim()))
        {
            sb2.AppendFormat("txtCRP={0}|", txtCRP.Text);
        }
        if (!string.IsNullOrEmpty(txtproBNP.Text.Trim()))
        {
            sb2.AppendFormat("txtproBNP={0}|", txtproBNP.Text);
        }
        if (!string.IsNullOrEmpty(txtBNP.Text.Trim()))
        {
            sb2.AppendFormat("txtBNP={0}|", txtBNP.Text);
        }
        if (!string.IsNullOrEmpty(txtMICRO.Text.Trim()))
        {
            sb2.AppendFormat("txtMICRO={0}|", txtMICRO.Text);
        }

        //Diagnostics
        if (chkEKG.Checked)
        {
            sb2.Append("chkEKG|");
            if (!string.IsNullOrEmpty(txtPRduration.Text.Trim()))
            {


                sb2.AppendFormat("txtPRduration={0}|", txtPRduration.Text);
            }
            if (!string.IsNullOrEmpty(txtQRS.Text.Trim()))
            {
                sb2.AppendFormat("txtQRS={0}|", txtQRS.Text);
            }
            if (!string.IsNullOrEmpty(txtQRSAx.Text.Trim()))
            {
                sb2.AppendFormat("txtQRSAx={0}|", txtQRSAx.Text);
            }
            if (!string.IsNullOrEmpty(txtQTc.Text.Trim()))
            {
                sb2.AppendFormat("txtQTc={0}|", txtQTc.Text);
            }
            if (ChkST.Checked)
            {
                sb2.Append("chkST|");
            }
            if (ChkTWI.Checked)
            {
                sb2.Append("chkTWI|");
            }
            if (Chknonspesific.Checked)
            {
                sb2.Append("chknonspesific|");
            }
            if (Chklowv.Checked)
            {
                sb2.Append("chklowv|");
            }
            if (Chkabnormq.Checked)
            {
                sb2.Append("chkabnormq|");
            }
            if (ChkLBBB.Checked)
            {
                sb2.Append("chkLBBB|");
                if (chkbifascicular.Checked)
                {
                    sb2.Append("chkbifascicular|");
                }
                if (chkLAHB.Checked)
                {
                    sb2.Append("chkLAHB|");
                }
                if (chkLPHB.Checked)
                {
                    sb2.Append("chkLPHB|");
                }
            }
            if (ChkLVH.Checked)
            {
                sb2.Append("chkLVH|");
            }
            if (ChkAPCs.Checked)
            {
                sb2.Append("chkAPCs|");
            }
            if (ChkJPDs.Checked)
            {
                sb2.Append("chkJPDs|");
            }
            if (ChkFreqVentPac.Checked)
            {
                sb2.Append("chkFreqVentPac|");
            }
            if (Chkinfreqvent.Checked)
            {
                sb2.Append("chkinfreqvent|");
            }
            if (ChkWPW.Checked)
            {
                sb2.Append("chkWPW|");
            }
            if (Chkatrialfib.Checked)
            {
                sb2.Append("chkatrialfib|");
            }
            if (ChkPSVT.Checked)
            {
                sb2.Append("chkPSVT|");
            }
            if (ChKRBBB.Checked)
            {
                sb2.Append("chkRBBB|");
            }
            if (ChkNSR.Checked)
            {
                sb2.Append("chkNSR|");
            }
            if (ChkVPace.Checked)
            {
                sb2.Append("chkVPace|");
            }
            if (ChkIVCD.Checked)
            {
                sb2.Append("chkIVCD|");
            }
        }
        if (chkDTS.Checked)
        {
            sb2.Append("chkDTS|");
            if (!string.IsNullOrEmpty(TxtTTS.Text.Trim()))
            {
                sb2.AppendFormat("TxtTTS={0}|", TxtTTS.Text);
            }
            if (!string.IsNullOrEmpty(txtStressSummedScore.Text.Trim()))
            {
                sb2.AppendFormat("txtStressSummedScore={0}|", txtStressSummedScore.Text);
            }
            if (!string.IsNullOrEmpty(txtDSE.Text.Trim()))
            {
                sb2.AppendFormat("txtDSE={0}|", txtDSE.Text);
            }
            if (!string.IsNullOrEmpty(txtreversible.Text.Trim()))
            {
                sb2.AppendFormat("txtreversible={0}|", txtreversible.Text);
            }
            if (!string.IsNullOrEmpty(txtWMA.Text.Trim()))
            {
                sb2.AppendFormat("txtWMA={0}|", txtWMA.Text);
            }
            if (!string.IsNullOrEmpty(txtMPS.Text.Trim()))
            {
                sb2.AppendFormat("txtMPS={0}|", txtMPS.Text);
            }
            if (!string.IsNullOrEmpty(txtExTimeMin.Text.Trim()))
            {
                sb2.AppendFormat("txtExTimeMin={0}|", txtExTimeMin.Text);
            }
            if (!string.IsNullOrEmpty(txtMaxST.Text.Trim()))
            {
                sb2.AppendFormat("txtMaxST={0}|", txtMaxST.Text);
            }
            if (!string.IsNullOrEmpty(rbAnginaIndex.Text))
            {
                sb2.AppendFormat("rbAnginaIndex={0}|", rbAnginaIndex.Text);
            }
            if (ChkSTelevation.Checked)
            {
                sb2.Append("chkSTelevation|");
            }
            if (ChkabnormalBP.Checked)
            {
                sb2.Append("chkabnormalBP|");
            }
            if (ChkExVT.Checked)
            {
                sb2.Append("chkExVT|");
            }
            if (ChkprolongedST.Checked)
            {
                sb2.Append("chkprolongedST|");
            }
            if (chkTID.Checked)
            {
                sb2.Append("chkTID|");
            }
            if (Chkfixed.Checked)
            {
                sb2.Append("chkfixed|");
                if (Chkviable.Checked)
                {
                    sb2.Append("chkviable|");
                }
            }
            if (Chkuninterpret.Checked)
            {
                sb2.Append("chkuninterpret|");
            }
        }
        if (chkEcho.Checked)
        {
            sb2.Append("chkEcho|");
            if (chkpericardial.Checked)
            {
                sb2.Append("chkpericardial|");
            }
            if (chkRWMA.Checked)
            {
                sb2.Append("chkRWMA|");
            }
            if (chkEnlargedLA.Checked)
            {
                sb2.Append("chkEnlargedLA|");
            }
            if (chkLVeccenticity.Checked)
            {
                sb2.Append("chkLVeccenticity|");
            }
            if (chkMPI.Checked)
            {
                sb2.Append("chkMPI|");
            }
            if (ChkIVSshift.Checked)
            {
                sb2.Append("chkIVSshift|");
            }
            if (ChkmitralE.Checked)
            {
                sb2.Append("chkmitralE|");
            }
            if (Chkhepatic.Checked)
            {
                sb2.Append("chkhepatic|");
            }
            if (ChkEnlargedRA.Checked)
            {
                sb2.Append("chkEnlargedRA|");
            }
            if (ChkEnlargedRV.Checked)
            {
                sb2.Append("chkEnlargedRV|");
            }
            if (Chkmidsystolicn.Checked)
            {
                sb2.Append("chkmidsystolicn|");
            }
            if (!string.IsNullOrEmpty(txtMP.Text.Trim()))
            {
                sb2.AppendFormat("txtMP={0}|", txtMP.Text);
            }
            if (!string.IsNullOrEmpty(txtEF.Text.Trim()))
            {
                sb2.AppendFormat("txtEF={0}|", txtEF.Text);
            }
            if (!string.IsNullOrEmpty(txtTRJ.Text.Trim()))
            {
                sb2.AppendFormat("txtTRJ={0}|", txtTRJ.Text);
            }
        }
        if (chkchestxray.Checked)
        {
            sb2.Append("chkchestxray|");
            if (chkRPLE.Checked)
            {
                sb2.Append("chkRPLE|");
            }
            if (chkLPLE.Checked)
            {
                sb2.Append("chkLPLE|");
            }
            if (chkpuledema.Checked)
            {
                sb2.Append("chkpuledema|");
            }
            if (chkenlarcar.Checked)
            {
                sb2.Append("chkenlarcar|");
            }
            if (chkWidened.Checked)
            {
                sb2.Append("chkWidened|");
            }
            if (chkpneumothorax.Checked)
            {
                sb2.Append("chkpneumothorax|");
            }
            if (chkinfiltrate.Checked)
            {
                sb2.Append("chkinfiltrate|");
            }
            if (chkconsolidation.Checked)
            {
                sb2.Append("chkconsolidation|");
            }
        }
        if (ChkHC.Checked)
        {
            sb2.Append("chkHC|");
        }
        if (!string.IsNullOrEmpty(txtCCS.Text.Trim()))
        {
            sb2.AppendFormat("txtCCS={0}|", txtCCS.Text);
        }
        if (!string.IsNullOrEmpty(txtTotalCCS.Text.Trim()))
        {
            sb2.AppendFormat("txtTotalCCS={0}|", txtTotalCCS.Text);
        }

        dcmd.Parameters.Add(new Param("@REST", sb2.ToString(), dbTypes.CHAR));

        dcmd.OleCommandText = sb1.ToString();
        int intRetVal = dw.ExecuteDataCommand(dcmd);
        if (intRetVal > 0)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "Evaluation saved successfuly.";
            lblMessage.BackColor = System.Drawing.Color.LightGreen;
        }
        else
        {
            lblMessage.Visible = true;
            lblMessage.Text = "Evaluation save error!";
            lblMessage.BackColor = System.Drawing.Color.LightPink;
        }
    }

    protected void Fill_CalcVal0(int id)
    {
        if (id == 0)
        {
            return;
        }
        dataWork dw = new dataWork(dbNames.OLEDB);
        dw.ConnectionString = CVDevaluator.SharedFunctions.GetConnStr("connString3");
        dataCommand dcmd = new dataCommand();
        dcmd.commandExecuteType = commandExecuteType.SELECT_DATATABLE;
        dcmd.OleCommandText = "SELECT * FROM Patients WHERE [ID]=@ID and User_ID=@USER_ID";
        dcmd.Parameters.Add(new Param("@ID", id.ToString(), dbTypes.CHAR));
        dcmd.Parameters.Add(new Param("@USER_ID", Session["USER0_ID"], dbTypes.INT32));

        int retVal = dw.ExecuteDataCommand(dcmd);
        if (dcmd.commandResult is System.Data.DataTable)
        {
            System.Data.DataTable dtResult = (DataTable)dcmd.commandResult;
            if (dtResult.Rows.Count == 0)
            {
                return;
            }

            string rest = "";
            try
            {
                txtFirstName.Text = dtResult.Rows[0]["FullName"].ToString().Trim();
                txtAge.Text = dtResult.Rows[0]["AGE"].ToString().Trim();
                rblGender.SelectedValue = dtResult.Rows[0]["GENDER"].ToString().Trim();
                txtSBP.Text = dtResult.Rows[0]["SBP"].ToString().Trim();
                txtDBP.Text = dtResult.Rows[0]["DBP"].ToString().Trim();
                rest = dtResult.Rows[0]["Rest"].ToString().Trim();

			}
			catch (Exception ex)
			{
			}

            string[] entries = rest.Split(new char[] { '|' });
            string[] textEntry = null;
            string entry = "";
            bool ischkbox = false;
            foreach (string entry_loopVariable in entries)
            {
                entry = entry_loopVariable;
                ischkbox = false;
                if (entry.Length > 3)
                {
                    if (entry.Substring(0, 3) == "chk")
                    {
                        ischkbox = true;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(entry))
                    {
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }

                if (ischkbox)
                {
                    switch (entry)
                    {
                        case "chkAsianCode":
                            chkAsianCode.Checked = true;
                            break;
                        case "chkSymptomatic":
                            chkSymptomatic.Checked = true;
                            break;
                        case "chkpreg":
                            chkpreg.Checked = true;
                            break;
                        case "chkHFPLVF":
                            ChKHFPLVF.Checked = true;
                            break;
                        case "chklung":
                            Chklung.Checked = true;
                            break;
                        case "chkasthma":
                            chkasthma.Checked = true;
                            pnlasthma.Style.Remove("display");
                            break;
                        case "chkinterference":
                            chkinterference.Checked = true;
                            pnlinterference.Style.Remove("display");
                            break;
                        case "chknone":
                            chknone.Checked = true;
                            break;
                        case "chkminor":
                            chkminor.Checked = true;
                            break;
                        case "chksome":
                            chksome.Checked = true;
                            break;
                        case "chksignificant":
                            chksignificant.Checked = true;
                            break;
                        case "chkILD":
                            chkILD.Checked = true;
                            break;
                        case "chkCOPD":
                            chkCOPD.Checked = true;
                            break;
                        case "chknewHF":
                            ChknewHF.Checked = true;
                            break;
                        case "chkD1":
                            ChkD1.Checked = true;
                            break;
                        case "chkD2":
                            ChkD2.Checked = true;
                            break;
                        case "chkD3":
                            ChkD3.Checked = true;
                            break;
                        case "chkD4":
                            ChkD4.Checked = true;
                            break;
                        case "chkD5":
                            ChkD5.Checked = true;
                            break;
                        case "chkfoutpt":
                            Chkfoutpt.Checked = true;
                            break;
                        case "chkICD":
                            chkICD.Checked = true;
                            break;
                        case "chkDOE":
                            ChkDOE.Checked = true;
                            PanelDOE.Style.Remove("display");
                            break;
                        case "chktachycardic":
                            chktachycardic.Checked = true;
                            Paneltachycardic.Style.Remove("display");
                            break;
                        case "chkvomiting":
                            chkvomiting.Checked = true;
                            Panelvomiting.Style.Remove("display");
                            break;
                        case "chkCyanosis":
                            chkCyanosis.Checked = true;
                            pnlCyanosis.Style.Remove("display");
                            break;
                        case "chkskin":
                            chkskin.Checked = true;
                            Panelskin.Style.Remove("display");
                            break;
                        case "chkclass1":
                            chkclass1.Checked = true;
                            break;
                        case "chkclass2":
                            chkclass2.Checked = true;
                            break;
                        case "chkclass3":
                            chkclass3.Checked = true;
                            break;
                        case "chkdysp":
                            Chkdysp.Checked = true;
                            break;
                        case "chkWeight":
                            ChkWeight.Checked = true;
                            break;
                        case "chkPEd":
                            ChkPEd.Checked = true;
                            break;
                        case "chkthorax":
                            ChkPEd.Checked = true;
                            break;
                        case "chkpercussion":
                            ChkPEd.Checked = true;
                            break;
                        case "chkrhonchi":
                            ChkPEd.Checked = true;
                            break;
                        case "chkS3":
                            ChkS3.Checked = true;
                            break;
                        case "chkPMI":
                            ChkPMI.Checked = true;
                            break;
                        case "chkMR":
                            ChkMR.Checked = true;
                            break;
                        case "chkhepato":
                            Chkhepato.Checked = true;
                            break;
                        case "chkJVD":
                            ChkJVD.Checked = true;
                            break;
                        case "chkedema":
                            Chkedema.Checked = true;
                            break;
                        case "chkAMS":
                            ChkAMS.Checked = true;
                            break;
                        case "chkS4":
                            ChkS4.Checked = true;
                            break;
                        case "chkbruit":
                            Chkbruit.Checked = true;
                            break;
                        case "chkascites":
                            Chkascites.Checked = true;
                            break;
                        case "chkHJR":
                            ChkHJR.Checked = true;
                            break;
                        case "chkdistant":
                            Chkdistant.Checked = true;
                            break;
                        case "chkWG":
                            ChkWG.Checked = true;
                            break;
                        case "chkshock":
                            Chkshock.Checked = true;
                            break;
                        case "chkskinwarm":
                            Chkskinwarm.Checked = true;
                            break;
                        case "chkNonischemicCMP":
                            ChkNonischemicCMP.Checked = true;
                            break;
                        case "chkMI":
                            chkMI.Checked = true;
                            pnlPostMI.Style.Remove("display");
                            break;
                        case "chkLVaneurysm":
                            chkLVaneurysm.Checked = true;
                            break;
                        case "chkIschemic":
                            ChkIschemic.Checked = true;
                            break;
                        case "chkCardiotoxins":
                            ChkCardiotoxins.Checked = true;
                            break;
                        case "chkFamHXofCMP":
                            ChkFamHXofCMP.Checked = true;
                            break;
                        case "chkmyocarditis":
                            Chkmyocarditis.Checked = true;
                            break;
                        case "chkSuspectedRVDysplasia":
                            ChkSuspectedRVDysplasia.Checked = true;
                            break;
                        case "chkIschemicCMP":
                            ChkIschemicCMP.Checked = true;
                            pnlIschemicCMP.Style.Remove("display");
                            break;
                        case "chkLVHGreater30":
                            ChkLVHGreater30.Checked = true;
                            break;
                        case "chkAbnormBP":
                            ChkAbnormBP.Checked = true;
                            break;
                        case "chkFamHistSuddenDeath":
                            ChkFamHistSuddenDeath.Checked = true;
                            break;
                        case "chkrestrictive":
                            Chkrestrictive.Checked = true;
                            break;
                        case "chkA":
                            chkA.Checked = true;
                            break;
                        case "chkB":
                            chkB.Checked = true;
                            break;
                        case "chkC":
                            chkC.Checked = true;
                            break;
                        case "chkD":
                            chkD.Checked = true;
                            pnlD.Style.Remove("display");
                            break;
                        case "chkcom":
                            Chkcom.Checked = true;
                            break;
                        case "chkNYHA1":
                            ChkNYHA1.Checked = true;
                            ChkNYHA1_2.Checked = true;
                            break;
                        case "chkNYHA2":
                            ChkNYHA2.Checked = true;
                            ChkNYHA2_2.Checked = true;
                            break;
                        case "chkNYHA3":
                            ChkNYHA3.Checked = true;
                            ChkNYHA3_2.Checked = true;
                            break;
                        case "chkNYHA4":
                            ChkNYHA4.Checked = true;
                            ChkNYHA4_2.Checked = true;
                            break;
                        case "chkIVAA":
                            ChkIVAA.Checked = true;
                            pnlIVAA.Style.Remove("display");
                            break;
                        case "chkAAcon":
                            ChkAAcon.Checked = true;
                            break;
                        case "chkAAbolus":
                            ChkAAbolus.Checked = true;
                            break;
                        case "chkAAtitr":
                            ChkAAtitr.Checked = true;
                            break;
                        case "chkPOAA":
                            ChkPOAA.Checked = true;
                            break;
                        case "chkurgcv":
                            Chkurgcv.Checked = true;
                            break;
                        case "chkdefib":
                            Chkdefib.Checked = true;
                            break;
                        case "chkIVHT":
                            ChkIVHT.Checked = true;
                            pnlIVHT.Style.Remove("display");
                            break;
                        case "chkHTcon":
                            ChkHTcon.Checked = true;
                            break;
                        case "chkHTbolus":
                            ChkHTbolus.Checked = true;
                            break;
                        case "chkHTtitr":
                            ChkHTtitr.Checked = true;
                            break;
                        case "chkIVVA":
                            ChkIVVA.Checked = true;
                            pnlIVVA.Style.Remove("display");
                            break;
                        case "chkVAcon":
                            ChkVAcon.Checked = true;
                            break;
                        case "chkVAbolus":
                            ChkVAbolus.Checked = true;
                            break;
                        case "chkVAtitr":
                            ChkVAtitr.Checked = true;
                            break;
                        case "chkIVNPS":
                            ChkIVNPS.Checked = true;
                            break;
                        case "chkIVNTG":
                            ChkIVNTG.Checked = true;
                            break;
                        case "chkmilrinone":
                            Chkmilrinone.Checked = true;
                            break;
                        case "chkIVDI":
                            ChkIVDI.Checked = true;
                            pnlIVDI.Style.Remove("display");
                            break;
                        case "chkcon":
                            Chkcon.Checked = true;
                            break;
                        case "chkPODI":
                            ChkPODI.Checked = true;
                            break;
                        case "chkNIPPV":
                            ChkNIPPV.Checked = true;
                            pnlNIPPV.Style.Remove("display");
                            break;
                        case "chkVAD":
                            ChkVAD.Checked = true;
                            break;
                        case "chkult":
                            chkult.Checked = true;
                            break;
                        case "chkIABP":
                            ChkIABP.Checked = true;
                            break;
                        case "chkTPM":
                            ChkTPM.Checked = true;
                            break;
                        case "chkACS":
                            chkACS.Checked = true;
                            pnlACS.Style.Remove("display");
                            break;
                        case "chkang24":
                            Chkang24.Checked = true;
                            break;
                        case "chkAn20":
                            ChkAn20.Checked = true;
                            break;
                        case "chkASA":
                            ChkASA.Checked = true;
                            break;
                        case "chktrop":
                            Chktrop.Checked = true;
                            break;
                        case "chkAnAn":
                            ChkAnAn.Checked = true;
                            break;
                        case "chkSTEMI":
                            chkSTEMI.Checked = true;
                            pnlSTEMI.Style.Remove("display");
                            break;
                        case "chkanterior":
                            chkanterior.Checked = true;
                            break;
                        case "chkinferior":
                            chkinferior.Checked = true;
                            break;
                        case "chkother":
                            chkother.Checked = true;
                            break;
                        case "chkrefractory":
                            chkrefractory.Checked = true;
                            break;
                        case "chkchangetrop":
                            chkchangetrop.Checked = true;
                            break;
                        case "chkPCI":
                            chkPCI.Checked = true;
                            pnlPCI.Style.Remove("display");
                            break;
                        case "chkBMS":
                            ChkBMS.Checked = true;
                            break;
                        case "chkDES":
                            ChkDES.Checked = true;
                            break;
                        case "chkCABG":
                            ChkCABG.Checked = true;
                            pnlCABG.Style.Remove("display");
                            break;
                        case "chkautovein":
                            chkautovein.Checked = true;
                            break;
                        case "chkautoartery":
                            chkautoartery.Checked = true;
                            break;
                        case "chknonauto":
                            chknonauto.Checked = true;
                            break;
                        case "chkSTENUSIS":
                            chkSTENUSIS.Checked = true;
                            pnlStableAng.Style.Remove("display");
                            break;
                        case "chkLM":
                            chkLM.Checked = true;
                            break;
                        case "chkLAD":
                            chkLAD.Checked = true;
                            break;
                        case "chkRCA":
                            ChkRCA.Checked = true;
                            break;
                        case "chkLCX":
                            ChkLCX.Checked = true;
                            break;
                        case "chkCTO":
                            chkCTO.Checked = true;
                            break;
                        case "chkcoraneurysm":
                            Chkcoraneurysm.Checked = true;
                            break;
                        case "chkcordissection":
                            Chkcordissection.Checked = true;
                            break;
                        case "chkcalcified":
                            Chkcalcified.Checked = true;
                            break;
                        case "chklipidrich":
                            Chklipidrich.Checked = true;
                            break;
                        case "chkvasospasm":
                            Chkvasospasm.Checked = true;
                            break;
                        case "chkparoxysmal":
                            Chkparoxysmal.Checked = true;
                            break;
                        case "chkpersistent":
                            Chkpersistent.Checked = true;
                            break;
                        case "chkchronicAF":
                            ChkchronicAF.Checked = true;
                            break;
                        case "chkpharm":
                            Chkpharm.Checked = true;
                            break;
                        case "chkVHD":
                            ChkVHD.Checked = true;
                            pnlVHD.Style.Remove("display");
                            break;
                        case "chkacutely":
                            Chkacutely.Checked = true;
                            break;
                        case "chkCV":
                            ChkCV.Checked = true;
                            break;
                        case "chkCAA":
                            chkCAA.Checked = true;
                            pnlCAA.Style.Remove("display");
                            break;
                        case "chkclass1AA":
                            chkclass1AA.Checked = true;
                            break;
                        case "chkclass3AA":
                            chkclass3AA.Checked = true;
                            break;
                        case "chkliver":
                            Chkliver.Checked = true;
                            break;
                        case "chkablation":
                            Chkablation.Checked = true;
                            break;
                        case "chkPrevious":
                            chkPrevious.Checked = true;
                            pnlPrevious.Style.Remove("display");
                            break;
                        case "chkThyro":
                            chkThyro.Checked = true;
                            break;
                        case "chkHypoThyro":
                            chkHypoThyro.Checked = true;
                            break;
                        case "chkflutter":
                            Chkflutter.Checked = true;
                            pnlflutter.Style.Remove("display");
                            break;
                        case "chktypicalAF":
                            chktypicalAF.Checked = true;
                            break;
                        case "chkatypicalAF":
                            chkatypicalAF.Checked = true;
                            break;
                        case "chksecondegree":
                            Chksecondegree.Checked = true;
                            pnlsecondegree.Style.Remove("display");

                            break;
                        case "chktype1":
                            Chktype1.Checked = true;
                            break;
                        case "chktype2":
                            Chktype2.Checked = true;
                            break;
                        case "chkbldrisk":
                            chkbldrisk.Checked = true;
                            pnlBleed.Style.Remove("display");
                            break;
                        case "chkPUD":
                            ChkPUD.Checked = true;
                            break;
                        case "chkTIA":
                            ChkTIA.Checked = true;
                            break;
                        case "chkorthopnea":
                            Chkorthopnea.Checked = true;
                            break;
                        case "chkPND":
                            ChkPND.Checked = true;
                            break;
                        case "chkpalpitations":
                            Chkpalpitations.Checked = true;
                            break;
                        case "chkDVTE":
                            ChkDVTE.Checked = true;
                            break;
                        case "chkbleed":
                            Chkbleed.Checked = true;
                            break;
                        case "chkplt":
                            Chkplt.Checked = true;
                            break;
                        case "chkICU":
                            ChkICU.Checked = true;
                            break;
                        case "chkCVC":
                            ChkCVC.Checked = true;
                            break;
                        case "chkrheum":
                            Chkrheum.Checked = true;
                            break;
                        case "chkVKA":
                            ChkVKA.Checked = true;
                            break;
                        case "chkCAC":
                            ChkCAC.Checked = true;
                            break;
                        case "chkFactorX":
                            chkFactorX.Checked = true;
                            break;
                        case "chkDeepVenousThrombosis":
                            chkDeepVenousThrombosis.Checked = true;
                            break;
                        case "chkDeepVenous":
                            chkDeepVenous.Checked = true;
                            break;
                        case "chksaddle":
                            chksaddle.Checked = true;
                            break;
                        case "chkotherPE":
                            chkotherPE.Checked = true;
                            break;
                        case "chkseptic":
                            chkseptic.Checked = true;
                            break;
                        case "chkprior":
                            chkprior.Checked = true;
                            break;
                        case "chkpriorASA":
                            chkpriorASA.Checked = true;
                            break;
                        case "chkDVTprevention":
                            chkDVTprevention.Checked = true;
                            pnlDVTPrevention.Style.Remove("display");
                            break;
                        case "chkPE":
                            chkPE.Checked = true;
                            pnlPE.Style.Remove("display");
                            break;
                        case "chkgeneral":
                            Chkgeneral.Checked = true;
                            break;
                        case "chkurology":
                            Chkurology.Checked = true;
                            break;
                        case "chkknee":
                            Chkknee.Checked = true;
                            break;
                        case "chkhip":
                            Chkhip.Checked = true;
                            break;
                        case "chkHPS":
                            ChkHPS.Checked = true;
                            break;
                        case "chkcancer":
                            Chkcancer.Checked = true;
                            break;
                        case "chkspinal":
                            Chkspinal.Checked = true;
                            break;
                        case "chkacute":
                            Chkacute.Checked = true;
                            pnlMedicalIllness.Style.Remove("display");
                            break;
                        case "chkhemoptysis":
                            Chkhemoptysis.Checked = true;
                            break;
                        case "chkunilateral":
                            Chkunilateral.Checked = true;
                            break;
                        case "chkulcer":
                            chkulcer.Checked = true;
                            break;
                        case "chkswelling":
                            Chkswelling.Checked = true;
                            break;
                        case "chkactivecancer":
                            Chkactivecancer.Checked = true;
                            break;
                        case "chkPVTE":
                            ChkPVTE.Checked = true;
                            break;
                        case "chkRF":
                            ChkRF.Checked = true;
                            break;
                        case "chkthrombo":
                            Chkthrombo.Checked = true;
                            break;
                        case "chkRecent":
                            ChkRecent.Checked = true;
                            break;
                        case "chkReduced":
                            ChkReduced.Checked = true;
                            break;
                        case "chkhormone":
                            Chkhormone.Checked = true;
                            break;
                        case "chkinfection":
                            Chkinfection.Checked = true;
                            break;
                        case "chkacutecva":
                            chkacutecva.Checked = true;
                            pnlAcuteCVA.Style.Remove("display");
                            break;
                        case "chk1":
                            CheckBox1.Checked = true;
                            break;
                        case "chk2":
                            CheckBox2.Checked = true;
                            break;
                        case "chkpreh":
                            chkpreh.Checked = true;
                            break;
                        case "chkcage":
                            Chkcage.Checked = true;
                            break;
                        case "chkmech":
                            chkmech.Checked = true;
                            break;
                        case "chkMdav":
                            chkMdav.Checked = true;
                            break;
                        case "chkRMVD":
                            chkRMVD.Checked = true;
                            pnlRMVD.Style.Remove("display");
                            break;
                        case "chkLAthr":
                            ChkLAthr.Checked = true;
                            break;
                        case "chkbiop":
                            chkbiop.Checked = true;
                            break;
                        case "chksysemb":
                            Chksysemb.Checked = true;
                            break;
                        case "chkIE":
                            chkIE.Checked = true;
                            break;
                        case "chkLPVT":
                            chkLPVT.Checked = true;
                            break;
                        case "chkRPVT":
                            chkRPVT.Checked = true;
                            break;
                        case "chkArterialIschemic":
                            chkArterialIschemic.Checked = true;
                            break;
                        case "chkAorticArchAth":
                            chkAorticArchAth.Checked = true;
                            break;
                        case "chkLVt":
                            ChkLVt.Checked = true;
                            break;
                        case "chkPFO":
                            chkPFO.Checked = true;
                            break;
                        case "chkMVP":
                            chkMVP.Checked = true;
                            break;
                        case "chkMitralAnnular":
                            chkMitralAnnular.Checked = true;
                            break;
                        case "chkCalcificAortic":
                            chkCalcificAortic.Checked = true;
                            break;
                        case "chkHeparinIndThrom":
                            chkHeparinIndThrom.Checked = true;
                            break;
                        case "chkApoA1":
                            ChkApoA1.Checked = true;
                            break;
                        case "chkCerebralVenous":
                            chkCerebralVenous.Checked = true;
                            break;
                        case "chkminorpro":
                            chkminorpro.Checked = true;
                            break;
                        case "chksurgery":
                            chksurgery.Checked = true;
                            break;
                        case "chkemergencysurgery":
                            chkemergencysurgery.Checked = true;
                            break;
                        case "chkTestSyncope":
                            ChkTestSyncope.Checked = true;
                            pnlSyncope.Style.Remove("display");
                            break;
                        case "chkSynStanding":
                            chkSynStanding.Checked = true;
                            break;
                        case "chkSynProSitting":
                            chkSynProSitting.Checked = true;
                            break;
                        case "chkSynOther":
                            chkSynOther.Checked = true;
                            break;
                        case "chkSynneck":
                            chkSynneck.Checked = true;
                            break;
                        case "chkSynNausea":
                            chkSynNausea.Checked = true;
                            break;
                        case "chkSynDizziness":
                            chkSynDizziness.Checked = true;
                            break;
                        case "chkSynPalpitations":
                            chkSynPalpitations.Checked = true;
                            break;
                        case "chkSynProCP":
                            chkSynProCP.Checked = true;
                            break;
                        case "chkSynsporadic":
                            chkSynsporadic.Checked = true;
                            break;
                        case "chkSyntrauma":
                            chkSyntrauma.Checked = true;
                            break;
                        case "chkSynNone":
                            chkSynNone.Checked = true;
                            break;
                        case "chkSynDisor":
                            chkSynDisor.Checked = true;
                            break;
                        case "chkSynPara":
                            chkSynPara.Checked = true;
                            break;
                        case "chkSynPosCP":
                            chkSynPosCP.Checked = true;
                            break;
                        case "chkSynaura":
                            chkSynaura.Checked = true;
                            break;
                        case "chkSynfatigue":
                            chkSynfatigue.Checked = true;
                            break;
                        case "chkSynpallor":
                            chkSynpallor.Checked = true;
                            break;
                        case "chkSynposture":
                            chkSynposture.Checked = true;
                            break;
                        case "chkCPsharp":
                            chkSynposture.Checked = true;
                            break;
                        case "chkSinusNodeDysfunction":
                            ChkSinusNodeDysfunction.Checked = true;
                            break;
                        case "chkAVNodeDisease":
                            ChkAVNodeDisease.Checked = true;
                            break;
                        case "chkVasoDepressorSyncope":
                            ChkVasoDepressorSyncope.Checked = true;
                            break;
                        case "chkCarotidSinusHypersensitivity":
                            ChkCarotidSinusHypersensitivity.Checked = true;
                            break;
                        case "chkPosturalOrthostatisTac":
                            ChkPosturalOrthostatisTac.Checked = true;
                            break;
                        case "chkAutonomicFailureSyndrome":
                            ChkAutonomicFailureSyndrome.Checked = true;
                            break;
                        case "chkHistCardArrest":
                            ChkHistCardArrest.Checked = true;
                            break;
                        case "chkBrugada":
                            ChkBrugada.Checked = true;
                            break;
                        case "chkLongQT":
                            ChkLongQT.Checked = true;
                            break;
                        case "chksarc":
                            Chksarc.Checked = true;
                            break;
                        case "chkNSVT":
                            ChkNSVT.Checked = true;
                            break;
                        case "chkMVT":
                            chkMVT.Checked = true;
                            break;
                        case "chkRMVT":
                            chkRMVT.Checked = true;
                            break;
                        case "chkPVT":
                            chkPVT.Checked = true;
                            break;
                        case "chkTors":
                            chkTors.Checked = true;
                            break;
                        case "chkIVT":
                            chkIVT.Checked = true;
                            break;
                        case "chkidioVT":
                            chkidioVT.Checked = true;
                            break;
                        case "chkemergency":
                            Chkemergency.Checked = true;
                            break;
                        case "chkelective":
                            Chkelective.Checked = true;
                            break;
                        case "chkvascular":
                            Chkvascular.Checked = true;
                            break;
                        case "chklowriskelec":
                            Chklowriskelec.Checked = true;
                            break;
                        case "chkunab":
                            Chkunab.Checked = true;
                            break;
                        case "chkTA":
                            chkTA.Checked = true;
                            pnlTA.Style.Remove("display");
                            break;
                        case "chkescTA":
                            chkescTA.Checked = true;
                            break;
                        case "chkPA":
                            chkPA.Checked = true;
                            pnlTA.Style.Remove("display");
                            break;
                        case "chkNCP":
                            chkNCP.Checked = true;
                            pnlNCP.Style.Remove("display");
                            break;
                        case "chkpleur":
                            chkpleur.Checked = true;
                            pnlSubCP.Style.Remove("display");
                            break;
                        case "chkCPbreath":
                            chkCPbreath.Checked = true;
                            pnlSubCP.Style.Remove("display");
                            break;
                        case "chkPrecordial":
                            chkPrecordial.Checked = true;
                            pnlSubCP.Style.Remove("display");
                            break;
                        case "chkCPSharp":
                            chkCPSharp.Checked = true;
                            break;
                        case "chkCPPressure":
                            chkCPPressure.Checked = true;
                            break;
                        case "chkCPTightness":
                            chkCPTightness.Checked = true;
                            break;
                        case "chkCPSoreness":
                            chkCPSoreness.Checked = true;
                            break;
                        case "chktearing":
                            Chktearing.Checked = true;
                            break;
                        case "chkCPLying":
                            chkCPLying.Checked = true;
                            break;
                        case "chkCPBreathing":
                            chkCPBreathing.Checked = true;
                            break;
                        case "chkCPCoughing":
                            chkCPCoughing.Checked = true;
                            break;
                        case "chkCPExertion":
                            chkCPExertion.Checked = true;
                            break;
                        case "chkCPNTG":
                            chkCPNTG.Checked = true;
                            break;
                        case "chkCPRest":
                            chkCPRest.Checked = true;
                            break;
                        case "chkCPSitting":
                            chkCPSitting.Checked = true;
                            break;
                        case "chkCPlyingside":
                            chkCPlyingside.Checked = true;
                            break;
                        case "chkCPAnalgesics":
                            chkCPAnalgesics.Checked = true;
                            break;
                        case "chkCPJaw":
                            chkCPJaw.Checked = true;
                            break;
                        case "chkCPBack":
                            chkCPBack.Checked = true;
                            break;
                        case "chkCPArm":
                            chkCPArm.Checked = true;
                            break;
                        case "chkCPDyspnea":
                            chkCPDyspnea.Checked = true;
                            break;
                        case "chkCPNausea":
                            chkCPNausea.Checked = true;
                            break;
                        case "chkCPDiaph":
                            chkCPDiaph.Checked = true;
                            break;
                        case "chkCPFaint":
                            chkCPFaint.Checked = true;
                            break;
                        case "chkCPPalpitations":
                            chkCPpalpitations.Checked = true;
                            break;
                        case "chkCPseconds":
                            chkCPseconds.Checked = true;
                            break;
                        case "chkCPminutes":
                            chkCPminutes.Checked = true;
                            break;
                        case "chkCPhours":
                            chkCPhours.Checked = true;
                            break;
                        case "chkCPconstant":
                            chkCPconstant.Checked = true;
                            break;
                        case "chkrub":
                            chkrub.Checked = true;
                            break;
                        case "chkbreath":
                            chkbreath.Checked = true;
                            break;
                        case "chkabdominal":
                            chkabdominal.Checked = true;
                            break;
                        case "chkER":
                            chkER.Checked = true;
                            pnlER.Style.Remove("display");
                            break;
                        case "chkpositivetrop":
                            chkpositivetrop.Checked = true;
                            pnlpositivetrop.Style.Remove("display");
                            break;
                        case "chkangina":
                            chkangina.Checked = true;
                            break;
                        case "chkangina20":
                            chkangina20.Checked = true;
                            break;
                        case "chkASAuse":
                            chkASAuse.Checked = true;
                            break;
                       
                        case "chkunable":
                            chkunable.Checked = true;
                            break;
                        case "chkNoncardioCVA":
                            chkNoncardioCVA.Checked = true;
                            break;
                        case "chkNoncardioTIA":
                            chkNoncardioTIA.Checked = true;
                            break;
                        case "chkCEA":
                            chkCEA.Checked = true;
                            break;
                        case "chkABI":
                            chkABI.Checked = true;
                            break;
                        case "chkclaudication":
                            Chkclaudication.Checked = true;
                            break;
                        case "chkcritical":
                            Chkcritical.Checked = true;
                            break;
                        case "chkbypass":
                            Chkbypass.Checked = true;
                            break;
                        case "chkBKbypass":
                            ChkBKbypass.Checked = true;
                            break;
                        case "chkAADiameter":
                            chkAADiameter.Checked = true;
                            break;
                        case "chkthoracic":
                            chkthoracic.Checked = true;
                            break;
                        case "chkthoracoabdominal":
                            chkthoracoabdominal.Checked = true;
                            break;
                        case "chkvalvular":
                            Chkvalvular.Checked = true;
                            break;
                        case "chkHeartMurmur":
                            chkHeartMurmur.Checked = true;
                            pnlMurmur.Style.Remove("display");
                            break;
                        case "chkS1Mitral":
                            chkS1Mitral.Checked = true;
                            pnlS1Mitral.Style.Remove("display");
                            break;
                        case "chkS1MitralLoud":
                            chkS1Mitral.Checked = true;
                            break;
                        case "chkS1MitralNormal":
                            chkS1MitralNormal.Checked = true;
                            break;
                        case "chkS1MitralSoft":
                            chkS1MitralSoft.Checked = true;
                            break;
                        case "chkS1Tricuspid":
                            chkS1Tricuspid.Checked = true;
                            pnlS1Mitral.Style.Remove("display");
                            break;
                        case "chkS1TricuspidLoud":
                            chkS1TricuspidLoud.Checked = true;
                            break;
                        case "chkS1TricuspidNormal":
                            chkS1TricuspidNormal.Checked = true;
                            break;
                        case "chkS1TricuspidSoft":
                            chkS1TricuspidSoft.Checked = true;
                            break;
                        case "chkS2Aortic":
                            chkS2Aortic.Checked = true;
                            pnlS2Aortic.Style.Remove("display");
                            break;
                        case "chkS2MitralLoud":
                            chkS2MitralLoud.Checked = true;
                            break;
                        case "chkS2MitralNormal":
                            chkS2MitralNormal.Checked = true;
                            break;
                        case "chkS2MitralSoft":
                            chkS2MitralSoft.Checked = true;
                            break;
                        case "chkP2Pulmonic":
                            chkP2Pulmonic.Checked = true;
                            pnlP2Pulmonic.Style.Remove("display");
                            break;
                        case "chkP2PulmonicLoud":
                            chkP2PulmonicLoud.Checked = true;
                            break;
                        case "chkP2PulmonicNormal":
                            chkP2PulmonicNormal.Checked = true;
                            break;
                        case "chkP2PulmonicSoft":
                            chkP2PulmonicSoft.Checked = true;
                            break;
                        case "chkSystolicMurmur":
                            chkSystolicMurmur.Checked = true;
                            pnlSystolicMurmur.Style.Remove("display");
                            break;
                        case "chkCresDecres":
                            chkCresDecres.Checked = true;
                            pnlCresDecres.Style.Remove("display");
                            break;
                        case "chksquat":
                            chksquat.Checked = true;
                            
                            break;
                        case "chkEarlyMidPeak":
                            chkEarlyMidPeak.Checked = true;
                            break;
                        case "chkLatePeak":
                            chkLatePeak.Checked = true;
                            break;
                        case "chkPlateuShaped":
                            chkPlateuShaped.Checked = true;
                            pnlPlateuShaped.Style.Remove("display");
                            break;
                        case "chkHalosystolic":
                            chkHalosystolic.Checked = true;
                            break;
                        case "chkPansystolic":
                            chkPansystolic.Checked = true;
                            break;
                        case "chkMidsystolic":
                            chkMidsystolic.Checked = true;
                            break;
                        case "chkDiastolicMurmur":
                            chkDiastolicMurmur.Checked = true;
                            pnlDiastolicMurmur.Style.Remove("display");
                            break;
                        case "chkDecrescendo":
                            chkDecrescendo.Checked = true;
                            break;
                        case "chkRumble":
                            chkRumble.Checked = true;
                            break;
                        case "chkMitralOpeningSnap":
                            chkMitralOpeningSnap.Checked = true;
                            break;
                        case "chkDM":
                            chkDM.Checked = true;
                            pnlDM.Style.Remove("display");
                            break;
                        case "chkDM1":
                            chkDM1.Checked = true;
                            pnlDM.Style.Remove("display");
                            break;
                        case "chkgestational":
                            chkgestational.Checked = true;
                            break;
                        case "chkRetinopathy":
                            ChkRetinopathy.Checked = true;
                            break;
                        case "chkDMmono":
                            chkDMmono.Checked = true;
                            break;
                        case "chkDMpoly":
                            chkDMpoly.Checked = true;
                            break;
                        case "chkDMautonom":
                            chkDMautonom.Checked = true;
                            break;
                        case "chkDMNP":
                            chkDMNP.Checked = true;
                            break;
                        case "chkDMCKD":
                            chkDMCKD.Checked = true;
                            break;
                        case "chkDMother":
                            chkDMother.Checked = true;
                            break;
                        case "chkDMangio":
                            chkDMangio.Checked = true;
                            break;
                        case "chkDMgangrene":
                            chkDMgangrene.Checked = true;
                            break;
                        case "chkDMothercirc":
                            chkDMothercirc.Checked = true;
                            break;
                        case "chkDMarthro":
                            chkDMarthro.Checked = true;
                            break;
                        case "chkDMskin":
                            chkDMskin.Checked = true;
                            break;
                        case "chkDMoral":
                            chkDMoral.Checked = true;
                            break;
                        case "chkDMhypo":
                            chkDMhypo.Checked = true;
                            break;
                        case "chkDMhypocoma":
                            chkDMhypocoma.Checked = true;
                            break;
                        case "chkDMhyper":
                            chkDMhyper.Checked = true;
                           break;
                        case "chkDMothercomp":
                            chkDMothercomp.Checked = true;
                            break;
                        case "chkDMunspec":
                            chkDMunspec.Checked = true;
                            break;
                        case "chkDMwithout":
                            chkDMwithout.Checked = true;
                            break;
                        case "chkSBPTreated":
                            chkSBPTreated.Checked = true;
                            break;
                        case "chkAA":
                            chkAA.Checked = true;
                            break;
                        case "chkaldo":
                            Chkaldo.Checked = true;
                            break;
                        case "chkrenovas":
                            Chkrenovas.Checked = true;
                            break;
                        case "chkFMD":
                            ChkFMD.Checked = true;
                            break;
                        case "chkOSA":
                            ChkOSA.Checked = true;
                            break;
                        case "Chkcough":
                            Chkcough.Checked = true;
                            break;
                        case "Chksputum":
                            Chksputum.Checked = true;
                            break;
                        case "Chksinus":
                            Chksinus.Checked = true;
                            break;
                        case "chkhead":
                            Chkhead.Checked = true;
                            break;
                        case "chkepis":
                            Chkepis.Checked = true;
                            break;
                        case "chkCP":
                            ChkCP.Checked = true;
                            break;
                        case "chkSmoke":
                            chkSmoke.Checked = true;
                            break;
                        case "chkFamily":
                            chkFamily.Checked = true;
                            break;
                        case "chkCKD":
                            chkCKD.Checked = true;
                            break;
                        case "chkcar":
                            Chkcar.Checked = true;
                            pnlCar.Style.Remove("display");
                            break;
                        case "chkBisoprolol25":
                            chkBisoprolol25.Checked = true;
                            break;
                        case "chkCarvedilol625":
                            chkCarvedilol625.Checked = true;
                            break;
                        case "chkCarvedilol125":
                            chkCarvedilol125.Checked = true;
                            break;
                        case "chkCarvedilol25":
                            chkCarvedilol25.Checked = true;
                            break;
                        case "chkBisoprolol5":
                            chkBisoprolol5.Checked = true;
                            break;
                        case "chkMetoprololER50":
                            chkMetoprololER50.Checked = true;
                            break;
                        case "chkMetoprololER100":
                            chkMetoprololER100.Checked = true;
                            break;
                        case "chkBisoprolol10":
                            chkBisoprolol10.Checked = true;
                            break;
                        case "chkMetoprololER200":
                            chkMetoprololER200.Checked = true;
                            break;
                        case "chkace":
                            Chkace.Checked = true;
                            pnlace.Style.Remove("display");
                            break;
                        case "chklisinopril5":
                            chklisinopril5.Checked = true;
                            break;
                        case "chklisinopril10":
                            chklisinopril10.Checked = true;
                            break;
                        case "chklisinopril20":
                            chklisinopril20.Checked = true;
                            break;
                        case "chklosartan25":
                            chklosartan25.Checked = true;
                            break;
                        case "chklosartan50":
                            chklosartan50.Checked = true;
                            break;
                        case "chklosartan100":
                            chklosartan100.Checked = true;
                            break;
                        case "chkCCB":
                            ChkCCB.Checked = true;
                            break;
                        case "chkvasodilator":
                            Chkvasodilator.Checked = true;
                            break;
                        case "chkdiuretic":
                            Chkdiuretic.Checked = true;
                            pnldiuretic.Style.Remove("display");
                            break;
                        case "chkfurosemide40":
                            chkfurosemide40.Checked = true;
                            break;
                        case "chkfurosemide80":
                            chkfurosemide80.Checked = true;
                            break;
                        case "chkfurosemide81":
                            chkfurosemide81.Checked = true;
                            break;
                        case "chkbumex1":
                            chkbumex1.Checked = true;
                            break;
                        case "chkbumex2":
                            chkbumex2.Checked = true;
                            break;
                        case "chkbumex3":
                            chkbumex3.Checked = true;
                            break;
                        case "chktorsemide20":
                            chktorsemide20.Checked = true;
                            break;
                        case "chktorsemide40":
                            chktorsemide40.Checked = true;
                            break;
                        case "chktorsemide50":
                            chktorsemide50.Checked = true;
                            break;
                        case "chkHCTZ":
                            ChkHCTZ.Checked = true;
                            break;
                        case "chkindapamide":
                            Chkindapamide.Checked = true;
                            break;
                        case "chkchlorthalidone":
                            Chkchlorthalidone.Checked = true;
                            break;
                        case "chkstatint":
                            chkstatint.Checked = true;
                            break;
                        case "chkalready":
                            chkalready.Checked = true;
                            break;
                        case "chkEKG":
                            chkEKG.Checked = true;
                            pnlEKG.Style.Remove("display");
                            break;
                        case "chkST":
                            ChkST.Checked = true;
                            break;
                        case "chkTWI":
                            ChkTWI.Checked = true;
                            break;
                        case "chknonspesific":
                            Chknonspesific.Checked = true;
                            break;
                        case "chklowv":
                            Chklowv.Checked = true;
                            break;
                        case "chkabnormq":
                            Chkabnormq.Checked = true;
                            break;
                        case "chkLBBB":
                            ChkLBBB.Checked = true;
                            pnlLBBB.Style.Remove("display");
                            break;
                        case "chkbifascicular":
                            chkbifascicular.Checked = true;
                            break;
                        case "chkLAHB":
                            chkLAHB.Checked = true;
                            break;
                        case "chkLPHB":
                            chkLPHB.Checked = true;
                            break;
                        case "chkLVH":
                            ChkLVH.Checked = true;
                            break;
                        case "chkAPCs":
                            ChkAPCs.Checked = true;
                            break;
                        case "chkJPDs":
                            ChkJPDs.Checked = true;
                            break;
                        case "chkFreqVentPac":
                            ChkFreqVentPac.Checked = true;
                            break;
                        case "chkinfreqvent":
                            Chkinfreqvent.Checked = true;
                            break;
                        case "chkWPW":
                            ChkWPW.Checked = true;
                            break;
                        case "chkatrialfib":
                            Chkatrialfib.Checked = true;
                            break;
                        case "chkfirstdegree":
                            Chkfirstdegree.Checked = true;
                            break;
                        case "chkthirdegree":
                            Chkthirdegree.Checked = true;
                            break;
                        case "chkPSVT":
                            ChkPSVT.Checked = true;
                            break;
                        case "chkRBBB":
                            ChKRBBB.Checked = true;
                            break;
                        case "chkNSR":
                            ChkNSR.Checked = true;
                            break;
                        case "chkVpace":
                            ChkVPace.Checked = true;
                            break;
                        case "chkIVCD":
                            ChkIVCD.Checked = true;
                            break;
                        case "chkDTS":
                            chkDTS.Checked = true;
                            pnlDTS.Style.Remove("display");
                            break;
                        case "chkSTelevation":
                            ChkSTelevation.Checked = true;
                            break;
                        case "chkabnormalBP":
                            ChkabnormalBP.Checked = true;
                            break;
                        case "chkExVT":
                            ChkExVT.Checked = true;
                            break;
                        case "chkprolongedST":
                            ChkprolongedST.Checked = true;
                            break;
                        case "chkTID":
                            chkTID.Checked = true;
                            break;
                        case "chkfixed":
                            Chkfixed.Checked = true;
                            pnlfixed.Style.Remove("display");
                            break;
                        case "chkviable":
                            Chkviable.Checked = true;
                            break;
                        case "chkuninterpret":
                            Chkuninterpret.Checked = true;
                            break;
                        case "chkEcho":
                            chkEcho.Checked = true;
                            pnlEcho.Style.Remove("display");
                            break;
                        case "chkpericardial":
                            chkpericardial.Checked = true;
                            break;
                        case "chkRWMA":
                            chkRWMA.Checked = true;
                            break;
                        case "chkEnlargedLA":
                            chkEnlargedLA.Checked = true;
                            break;
                        case "chkLVeccenticity":
                            chkLVeccenticity.Checked = true;
                            break;
                        case "chkMPI":
                            chkMPI.Checked = true;
                            break;
                        case "chkIVSshift":
                            ChkIVSshift.Checked = true;
                            break;
                        case "chkmitralE":
                            ChkmitralE.Checked = true;
                            break;
                        case "chkhepatic":
                            Chkhepatic.Checked = true;
                            break;
                        case "chkEnlargedRA":
                            ChkEnlargedRA.Checked = true;
                            break;
                        case "chkEnlargedRV":
                            ChkEnlargedRV.Checked = true;
                            break;
                        case "chkmidsystolicn":
                            Chkmidsystolicn.Checked = true;
                            break;
                        case "chkchestxray":
                            chkchestxray.Checked = true;
                            break;
                        case "chkRPLE":
                            chkRPLE.Checked = true;
                            break;
                        case "chkLPLE":
                            chkLPLE.Checked = true;
                            break;
                        case "chkpuledema":
                            chkpuledema.Checked = true;
                            break;
                        case "chkenlarcar":
                            chkenlarcar.Checked = true;
                            break;
                        case "chkWidened":
                            chkWidened.Checked = true;
                            break;
                        case "chkpneumothorax":
                            chkpneumothorax.Checked = true;
                            break;
                        case "chkinfiltrate":
                            chkinfiltrate.Checked = true;
                            break;
                        case "chkconsolidation":
                            chkconsolidation.Checked = true;
                            break;
                        case "chkHC":
                            ChkHC.Checked = true;
                            break;
                    }
                }
                else
                {
                    textEntry = entry.Split('=');

                    switch (textEntry[0])
                    {
                        case "weight":
                            txtWeight.Text = textEntry[1];
                            break;
                        case "waist":
                            txtWaist.Text = textEntry[1];
                            break;
                        case "txtBMI":
                            txtBMI.Text = textEntry[1];
                            break;
                        case "OrthostaticSBP":
                            txtOrthostaticSBP.Text = textEntry[1];
                            break;
                        case "HR":
                            txtHR.Text = textEntry[1];
                            break;
                        case "ASBP":
                            txtASBP.Text = textEntry[1];
                            break;
                        case "ADBP":
                            txtADBP.Text = textEntry[1];
                            break;
                        case "txtdur":
                            txtdur.Text = textEntry[1];
                            break;
                        case "txtFEV1":
                            txtFEV1.Text = textEntry[1];
                            break;
                        case "txtFEVP":
                            txtFEVP.Text = textEntry[1];
                            break;
                        case "txtFVCP":
                            txtFVCP.Text = textEntry[1];
                            break;
                        case "txtDLCO":
                            txtDLCO.Text = textEntry[1];
                            break;
                        case "txtpalseconds":
                            txtdur.Text = textEntry[1];
                            break;
                        case "txtpalminutes":
                            txtdur.Text = textEntry[1];
                            break;
                        case "txtpalhours":
                            txtdur.Text = textEntry[1];
                            break;
                        case "txtCHF":
                            txtCHF.Text = textEntry[1];
                            break;
                        case "txtO2sat":
                            txtO2sat.Text = textEntry[1];
                            break;
                        case "TxtVO2":
                            TxtVO2.Text = textEntry[1];
                            break;
                        case "TxtAAmonitor":
                            TxtAAmonitor.Text = textEntry[1];
                            break;
                        case "TxtHTmonitor":
                            TxtHTmonitor.Text = textEntry[1];
                            break;
                        case "TxtVAmonitor":
                            TxtVAmonitor.Text = textEntry[1];
                            break;
                        case "Txtmonitor":
                            Txtmonitor.Text = textEntry[1];
                            break;
                        case "txtRI":
                            txtRI.Text = textEntry[1];
                            break;
                        case "txtOsupp":
                            txtOsupp.Text = textEntry[1];
                            break;
                        case "Txtprocedure":
                            Txtprocedure.Text = textEntry[1];
                            break;
                        case "TxtPrevious":
                            TxtPrevious.Text = textEntry[1];
                            break;
                        case "TxtHF":
                            TxtHF.Text = textEntry[1];
                            break;
                        case "txtNumVessel":
                            txtNumVessel.Text = textEntry[1];
                            break;
                        case "TxtSTEMIdur":
                            TxtSTEMIdur.Text = textEntry[1];
                            break;
                        case "Txtafd":
                            Txtafd.Text = textEntry[1];
                            break;
                        case "TxtCMP":
                            TxtCMP.Text = textEntry[1];
                            break;
                        case "txtINR":
                            txtINR.Text = textEntry[1];
                            break;
                        case "txtNIHSS":
                            txtNIHSS.Text = textEntry[1];
                            break;
                        case "ssdur":
                            ssdur.Text = textEntry[1];
                            break;
                        case "txtLAd":
                            txtLAd.Text = textEntry[1];
                            break;
                        case "txtSynMin":
                            txtSynMin.Text = textEntry[1];
                            break;
                        case "txtSynSec":
                            txtSynSec.Text = textEntry[1];
                            break;
                        case "TxtMETS":
                            TxtMETS.Text = textEntry[1];
                            break;
                        case "txtDiffInSBP":
                            txtDiffInSBP.Text = textEntry[1];
                            break;
                        case "TxtDASI":
                            TxtDASI.Text = textEntry[1];
                            break;
                        case "txtNameq":
                            txtNameq.Text = textEntry[1];
                            if (Convert.ToDouble(txtNameq.Text) > 129)
                            {
                                pnlNameq.Style.Remove("display");
                            }
                            break;
                        case "txtHomo":
                            txtHomo.Text = textEntry[1];
                            break;
                        case "txtcreatinine":
                            txtcreatinine.Text = textEntry[1];
                            break;
                        case "txtBUN":
                            txtBUN.Text = textEntry[1];
                            break;
                        case "txtGlucose":
                            txtGlucose.Text = textEntry[1];
                            break;
                        case "txtGFR":
                            txtGFR.Text = textEntry[1];
                            break;
                        case "txturineNa":
                            txturineNa.Text = textEntry[1];
                            break;
                        case "txtsosm":
                            txtsosm.Text = textEntry[1];
                            break;
                        case "txtuosm":
                            txtuosm.Text = textEntry[1];
                            break;
                        case "txtCholesterol":
                            txtCholesterol.Text = textEntry[1];
                            break;
                        case "txtTRG":
                            txtTRG.Text = textEntry[1];
                            break;
                        case "txtLDL":
                            txtLDL.Text = textEntry[1];
                            break;
                        case "txtHDL":
                            txtHDL.Text = textEntry[1];
                            break;
                        case "txtApoB":
                            txtApoB.Text = textEntry[1];
                            break;
                        case "txtLDLP":
                            txtLDLP.Text = textEntry[1];
                            break;
                        case "txtLPA":
                            txtLPA.Text = textEntry[1];
                            break;
                        case "txtASCVD":
                            txtASCVD.Text = textEntry[1];
                            break;
                        case "txtHBA1C":
                            txtHBA1C.Text = textEntry[1];
                            break;
                        case "txtCRP":
                            txtCRP.Text = textEntry[1];
                            break;
                        case "txtproBNP":
                            txtproBNP.Text = textEntry[1];
                            break;
                        case "txtBNP":
                            txtBNP.Text = textEntry[1];
                            break;
                        case "txtMICRO":
                            txtMICRO.Text = textEntry[1];
                            break;
                        case "txtPRduration":
                            txtPRduration.Text = textEntry[1];
                            break;
                        case "txtQRS":
                            txtQRS.Text = textEntry[1];
                            break;
                        case "txtQRSAx":
                            txtQRSAx.Text = textEntry[1];
                            break;
                        case "txtQTc":
                            txtQTc.Text = textEntry[1];
                            break;
                        case "TxtTTS":
                            TxtTTS.Text = textEntry[1];
                            break;
                        case "txtStressSummedScore":
                            txtStressSummedScore.Text = textEntry[1];
                            break;
                        case "txtDSE":
                            txtDSE.Text = textEntry[1];
                            break;
                        case "txtreversible":
                            txtreversible.Text = textEntry[1];
                            break;
                        case "txtWMA":
                            txtWMA.Text = textEntry[1];
                            break;
                        case "txtMPS":
                            txtMPS.Text = textEntry[1];
                            break;
                        case "txtExTimeMin":
                            txtExTimeMin.Text = textEntry[1];
                            break;
                        case "txtMaxST":
                            txtMaxST.Text = textEntry[1];
                            break;
                        case "rbAnginaIndex":
                            rbAnginaIndex.Text = textEntry[1];
                            break;
                        case "txtMP":
                            txtMP.Text = textEntry[1];
                            break;
                        case "txtEF":
                            txtEF.Text = textEntry[1];
                            break;
                        case "txtTRJ":
                            txtTRJ.Text = textEntry[1];
                            break;
                        case "txtCCS":
                            txtCCS.Text = textEntry[1];
                            break;
                        case "txtTotalCCS":
                            txtTotalCCS.Text = textEntry[1];
                            break;
                    }
                }
            }

        }

    }

    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        Save_CalcVal();
    }

    protected string isTrue(bool bln)
    {
        if (bln)
        {
            return "1";
        }
        else
        {
            return "0";
        }
    }

    protected void btnSwitchtoPAH_Click(object sender, System.EventArgs e)
    {
        int counter = 1;
        if (!string.IsNullOrEmpty(txtFirstName.Text))
        {
            Session[Convert.ToString(counter)] = "txtFirstName-" + txtFirstName.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtAge.Text))
        {
            Session[Convert.ToString(counter)] = "txtAge-" + txtAge.Text;
            counter += 1;
        }
        if (rblGender.SelectedIndex > -1)
        {
            Session[Convert.ToString(counter)] = "rblGender-" + rblGender.SelectedIndex;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtWeight.Text))
        {
            Session[Convert.ToString(counter)] = "txtWeight-" + txtWeight.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtSBP.Text))
        {
            Session[Convert.ToString(counter)] = "txtSBP-" + txtSBP.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtDBP.Text))
        {
            Session[Convert.ToString(counter)] = "txtDBP-" + txtDBP.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtHR.Text))
        {
            Session[Convert.ToString(counter)] = "txtHR-" + txtHR.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtASBP.Text))
        {
            Session[Convert.ToString(counter)] = "txtASBP-" + txtASBP.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtADBP.Text))
        {
            Session[Convert.ToString(counter)] = "txtADBP-" + txtADBP.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtNameq.Text))
        {
            Session[Convert.ToString(counter)] = "txtNa-" + txtNameq.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtHomo.Text))
        {
            Session[Convert.ToString(counter)] = "txtHomo-" + txtHomo.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtcreatinine.Text))
        {
            Session[Convert.ToString(counter)] = "txtcreatinine-" + txtcreatinine.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtBUN.Text))
        {
            Session[Convert.ToString(counter)] = "txtBUN-" + txtBUN.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtBNP.Text))
        {
            Session[Convert.ToString(counter)] = "txtBNP-" + txtBNP.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtGFR.Text))
        {
            Session[Convert.ToString(counter)] = "txtGFR-" + txtGFR.Text;
            counter += 1;
        }
        if (chkEKG.Checked)
        {
            Session[Convert.ToString(counter)] = "chkEKG-1";
            counter += 1;
        }
        if (chkEcho.Checked)
        {
            Session[Convert.ToString(counter)] = "chkECHO-1";
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtQRS.Text))
        {
            Session[Convert.ToString(counter)] = "txtQRS-" + txtQRS.Text;
            counter += 1;
        }
        if (ChkLBBB.Checked)
        {
            Session[Convert.ToString(counter)] = "ChkLBBB-1";
            counter += 1;
        }

        if (ChkLVH.Checked)
        {
            Session[Convert.ToString(counter)] = "ChkLVH-1";
            counter += 1;
        }
        if (Chkviable.Checked)
        {
            Session[Convert.ToString(counter)] = "pnlfixed-" + isTrue(Chkviable.Checked);
            counter += 1;
        }

        if (!string.IsNullOrEmpty(txtdur.Text))
        {
            Session[Convert.ToString(counter)] = "txtdur-" + txtdur.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtFEV1.Text))
        {
            Session[Convert.ToString(counter)] = "txtFEV1-" + txtFEV1.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtFEVP.Text))
        {
            Session[Convert.ToString(counter)] = "txtFEVP-" + txtFEVP.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtFVCP.Text))
        {
            Session[Convert.ToString(counter)] = "txtFVCP-" + txtFVCP.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtDLCO.Text))
        {
            Session[Convert.ToString(counter)] = "txtDLCO-" + txtDLCO.Text;
            counter += 1;
        }
        if (ChknewHF.Checked)
        {
            Session[Convert.ToString(counter)] = "ChknewHF-1";
            counter += 1;
        }
        if (ChkD1.Checked)
        {
            Session[Convert.ToString(counter)] = "ChkD1-1";
            counter += 1;
        }
        if (ChkD2.Checked)
        {
            Session[Convert.ToString(counter)] = "ChkD2-1";
            counter += 1;
        }
        if (ChkD3.Checked)
        {
            Session[Convert.ToString(counter)] = "ChkD3-1";
            counter += 1;
        }
        if (ChkD4.Checked)
        {
            Session[Convert.ToString(counter)] = "ChkD4-1";
            counter += 1;
        }
        if (ChkD5.Checked)
        {
            Session[Convert.ToString(counter)] = "ChkD5-1";
            counter += 1;
        }
        if (ChKHFPLVF.Checked)
        {
            Session[Convert.ToString(counter)] = "ChKHFPLVF-1";
            counter += 1;
        }
        if (Chklung.Checked)
        {
            Session[Convert.ToString(counter)] = "Chklung-1";
            counter += 1;
        }
        if (chkasthma.Checked)
        {
            Session[Convert.ToString(counter)] = "chkasthma-1";
            counter += 1;
        }
        if (chkILD.Checked)
        {
            Session[Convert.ToString(counter)] = "chkILD-1";
            counter += 1;
        }
        if (chkCOPD.Checked)
        {
            Session[Convert.ToString(counter)] = "chkCOPD-1";
            counter += 1;
        }
        if (Chkfoutpt.Checked)
        {
            Session[Convert.ToString(counter)] = "Chkfoutpt-1";
            counter += 1;
        }
        
        if (chkICD.Checked)
        {
            Session[Convert.ToString(counter)] = "chkICD-1";
            counter += 1;
        }
        //if (ChkD1.Checked | ChkD2.Checked | ChkD3.Checked | ChkD4.Checked | ChkD5.Checked | ChKHFPLVF.Checked | chkfoutpt.Checked | chkICD.Checked)
        //{
        //    Session[Convert.ToString(counter)] = "ChkHFDays." + isTrue(ChkD1.Checked) + isTrue(ChkD2.Checked) + isTrue(ChkD3.Checked) + isTrue(ChkD4.Checked) + isTrue(ChkD5.Checked) + isTrue(ChKHFPLVF.Checked) + isTrue(chkfoutpt.Checked) + isTrue(chkICD.Checked);
        //    counter += 1;
        //}
        if (ChkNonischemicCMP.Checked | ChkIschemicCMP.Checked | ChkIschemic.Checked | ChkCardiotoxins.Checked | ChkFamHXofCMP.Checked | Chkvalvular.Checked | ChkSuspectedRVDysplasia.Checked | chkMI.Checked)
        {
            Session[Convert.ToString(counter)] = "etiology-" + isTrue(ChkNonischemicCMP.Checked) + isTrue(ChkIschemicCMP.Checked) + isTrue(ChkIschemic.Checked) + isTrue(ChkCardiotoxins.Checked) + isTrue(ChkFamHXofCMP.Checked) + isTrue(Chkvalvular.Checked) + isTrue(ChkSuspectedRVDysplasia.Checked) + isTrue(chkMI.Checked);
            counter += 1;
        }
        if (ChkLVHGreater30.Checked | ChkAbnormBP.Checked | ChkFamHistSuddenDeath.Checked | ChkLVOT.Checked)
        {
            Session[Convert.ToString(counter)] = "pnlischem-" + isTrue(ChkLVHGreater30.Checked) + isTrue(ChkAbnormBP.Checked) + isTrue(ChkFamHistSuddenDeath.Checked) + isTrue(ChkLVOT.Checked);
            counter += 1;
        }
        if (chkclass1.Checked | chkclass2.Checked | chkclass3.Checked)
        {
            Session[Convert.ToString(counter)] = "pnlDOE-" + isTrue(chkclass1.Checked) + isTrue(chkclass2.Checked) + isTrue(chkclass3.Checked);
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtCHF.Text))
        {
            Session[Convert.ToString(counter)] = "txtCHF-" + txtCHF.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtO2sat.Text))
        {
            Session[Convert.ToString(counter)] = "txtO2sat-" + txtO2sat.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtproBNP.Text))
        {
            Session[Convert.ToString(counter)] = "txtproBNP-" + txtproBNP.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtBNP.Text))
        {
            Session[Convert.ToString(counter)] = "txtBNP-" + txtBNP.Text;
            counter += 1;
        }

        if (ChkPEd.Checked | ChkS3.Checked |  Chkshock.Checked | ChkDOE.Checked | Chkdysp.Checked | ChkMR.Checked |  Chkhepato.Checked | ChkJVD.Checked | ChkWeight.Checked | ChkAMS.Checked | ChkWG.Checked | Chkedema.Checked)
        {
            Session[Convert.ToString(counter)] = "clinical-" + isTrue(ChkPEd.Checked) + isTrue(ChkS3.Checked)  + isTrue(Chkshock.Checked) + isTrue(ChkDOE.Checked) + isTrue(Chkdysp.Checked)   + isTrue(ChkMR.Checked) + isTrue(Chkhepato.Checked) + isTrue(ChkJVD.Checked) + isTrue(ChkWeight.Checked) + isTrue(ChkAMS.Checked) + isTrue(ChkWG.Checked) + isTrue(Chkedema.Checked);
            counter += 1;
        }
        if (chkA.Checked | chkB.Checked | chkC.Checked | chkD.Checked | ChkNYHA1.Checked | ChkNYHA2.Checked | ChkNYHA3.Checked | ChkNYHA4.Checked | ChkNYHA1_2.Checked | ChkNYHA2_2.Checked | ChkNYHA3_2.Checked | ChkNYHA4_2.Checked)
        {
            Session[Convert.ToString(counter)] = "stage-" + isTrue(chkA.Checked) + isTrue(chkB.Checked) + isTrue(chkC.Checked) + isTrue(chkD.Checked) + isTrue(ChkNYHA1.Checked | ChkNYHA1_2.Checked) + isTrue(ChkNYHA2.Checked | ChkNYHA2_2.Checked) + isTrue(ChkNYHA3.Checked | ChkNYHA3_2.Checked) + isTrue(ChkNYHA4.Checked | ChkNYHA4_2.Checked);
            counter += 1;
        }
        if (Chkcom.Checked)
        {
            Session[Convert.ToString(counter)] = "Chkcom-1";
            counter += 1;
        }
        if (!string.IsNullOrEmpty(TxtVO2.Text))
        {
            Session[Convert.ToString(counter)] = "TxtVO2-" + TxtVO2.Text;
            counter += 1;
        }
        if (ChkIVAA.Checked | Chkurgcv.Checked | Chkdefib.Checked | ChkIVHT.Checked | ChkIVVA.Checked | ChkIVDI.Checked | ChkNIPPV.Checked | chkult.Checked | ChkIABP.Checked | ChkVAD.Checked | ChkTPM.Checked)
        {
            Session[Convert.ToString(counter)] = "Therap-" + isTrue(ChkIVAA.Checked) + isTrue(Chkurgcv.Checked) + isTrue(Chkdefib.Checked) + isTrue(ChkIVHT.Checked) + isTrue(ChkIVVA.Checked) + isTrue(ChkIVDI.Checked) + isTrue(ChkNIPPV.Checked) + isTrue(chkult.Checked) + isTrue(ChkIABP.Checked) + isTrue(ChkVAD.Checked) + isTrue(ChkTPM.Checked);
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtOsupp.Text))
        {
            Session[Convert.ToString(counter)] = "txtOsupp-" + txtOsupp.Text;
            counter += 1;
        }
        if (ChkAAcon.Checked | ChkAAbolus.Checked | ChkAAtitr.Checked | ChkPOAA.Checked)
        {
            Session[Convert.ToString(counter)] = "pnlIVAA-" + isTrue(ChkAAcon.Checked) + isTrue(ChkAAbolus.Checked) + isTrue(ChkAAtitr.Checked) + isTrue(ChkPOAA.Checked);
            counter += 1;
        }
        if (!string.IsNullOrEmpty(TxtAAmonitor.Text))
        {
            Session[Convert.ToString(counter)] = "TxtAAmonitor-" + TxtAAmonitor.Text;
            counter += 1;
        }
        if (ChkHTcon.Checked | ChkHTbolus.Checked | ChkHTtitr.Checked)
        {
            Session[Convert.ToString(counter)] = "pnlIVHT-" + isTrue(ChkHTcon.Checked) + isTrue(ChkHTbolus.Checked) + isTrue(ChkHTtitr.Checked);
            counter += 1;
        }
        if (!string.IsNullOrEmpty(TxtHTmonitor.Text))
        {
            Session[Convert.ToString(counter)] = "TxtHTmonitor-" + TxtHTmonitor.Text;
            counter += 1;
        }
        if (ChkVAcon.Checked | ChkVAbolus.Checked | ChkVAtitr.Checked | ChkIVNPS.Checked | ChkIVNTG.Checked | Chkmilrinone.Checked)
        {
            Session[Convert.ToString(counter)] = "pnlIVVA-" + isTrue(ChkVAcon.Checked) + isTrue(ChkVAbolus.Checked) + isTrue(ChkVAtitr.Checked) + isTrue(ChkIVNPS.Checked) + isTrue(ChkIVNTG.Checked) + isTrue(Chkmilrinone.Checked);
            counter += 1;
        }
        if (!string.IsNullOrEmpty(TxtVAmonitor.Text))
        {
            Session[Convert.ToString(counter)] = "TxtVAmonitor-" + TxtVAmonitor.Text;
            counter += 1;
        }
        if (Chkcon.Checked | ChkPODI.Checked)
        {
            Session[Convert.ToString(counter)] = "pnlIVDI-" + isTrue(Chkcon.Checked) + isTrue(ChkPODI.Checked);
            counter += 1;
        }
        if (!string.IsNullOrEmpty(Txtmonitor.Text))
        {
            Session[Convert.ToString(counter)] = "Txtmonitor-" + Txtmonitor.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtRI.Text))
        {
            Session[Convert.ToString(counter)] = "txtRI-" + txtRI.Text;
            counter += 1;
        }
        if (chkclass1.Checked | chkclass2.Checked | chkclass3.Checked)
        {
            Session[Convert.ToString(counter)] = "PanelDOE-" + isTrue(chkclass1.Checked) + isTrue(chkclass2.Checked) + isTrue(chkclass3.Checked);
            counter += 1;
        }

        if (chkACS.Checked | chkPCI.Checked | chkSTENUSIS.Checked)
        {
            Session[Convert.ToString(counter)] = "CHDs-" + isTrue(chkACS.Checked) + isTrue(chkPCI.Checked) + isTrue(chkSTENUSIS.Checked);
            counter += 1;
        }
        if (!string.IsNullOrEmpty(TxtCMP.Text))
        {
            Session[Convert.ToString(counter)] = "TxtCMP-" + TxtCMP.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(TxtPrevious.Text))
        {
            Session[Convert.ToString(counter)] = "TxtPrevious-" + TxtPrevious.Text;
            counter += 1;
        }
        if (chkclass1AA.Checked | chkclass3AA.Checked)
        {
            Session[Convert.ToString(counter)] = "pnlCAA-" + isTrue(chkclass1AA.Checked) + isTrue(chkclass3AA.Checked);
            counter += 1;
        }
        if (Chkacutely.Checked)
        {
            Session[Convert.ToString(counter)] = "pnlVHD-" + isTrue(Chkacutely.Checked);
            counter += 1;
        }
        if (Chkliver.Checked)
        {
            Session[Convert.ToString(counter)] = "Chkliver-" + isTrue(Chkliver.Checked);
            counter += 1;
        }
        if (Chkablation.Checked)
        {
            Session[Convert.ToString(counter)] = "Chkablation-" + isTrue(Chkablation.Checked);
            counter += 1;
        }
        if (ChkSinusNodeDysfunction.Checked | ChkAVNodeDisease.Checked | ChkCarotidSinusHypersensitivity.Checked)
        {
            Session[Convert.ToString(counter)] = "Syncopes-" + isTrue(ChkSinusNodeDysfunction.Checked) + isTrue(ChkAVNodeDisease.Checked) + isTrue(ChkCarotidSinusHypersensitivity.Checked);
            counter += 1;
        }
        if (ChkNSVT.Checked)
        {
            Session[Convert.ToString(counter)] = "ChkNSVT-1";
            counter += 1;
        }
        if (chkDM.Checked)
        {
            Session[Convert.ToString(counter)] = "chkDM-1";
            counter += 1;
        }
        if (chkSAH.Checked)
        {
            Session[Convert.ToString(counter)] = "chkSAH-1";
            counter += 1;
        }
        if (ChkOSA.Checked)
        {
            Session[Convert.ToString(counter)] = "ChkOSA-1";
            counter += 1;
        }
        if (Chkelective.Checked)
        {
            Session[Convert.ToString(counter)] = "chkelective-1";
            counter += 1;
        }
        if (Chkemergency.Checked)
        {
            Session[Convert.ToString(counter)] = "Chkemergency-1";
            counter += 1;
        }
        if (Chkvascular.Checked)
        {
            Session[Convert.ToString(counter)] = "Chkvascular-1";
            counter += 1;
        }
        if (!string.IsNullOrEmpty(TxtMETS.Text))
        {
            Session[Convert.ToString(counter)] = "TxtMETS-" + TxtMETS.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(TxtDASI.Text))
        {
            Session[Convert.ToString(counter)] = "TxtDASI-" + TxtDASI.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtDSE.Text))
        {
            Session[Convert.ToString(counter)] = "TxtDSE-" + txtDSE.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtreversible.Text))
        {
            Session[Convert.ToString(counter)] = "Txtreversible-" + txtreversible.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtWMA.Text))
        {
            Session[Convert.ToString(counter)] = "TxtWMA-" + txtWMA.Text;
            counter += 1;
        }
        if (!string.IsNullOrEmpty(txtMPS.Text))
        {
            Session[Convert.ToString(counter)] = "TxtMPS-" + txtMPS.Text;
            counter += 1;
        }
        //chkHF, chkCHD

        Response.Redirect("PatientPAH.aspx?FromPatient=1");
    }
    public Patient()
    {
        Load += Page_Load;
    }
}
