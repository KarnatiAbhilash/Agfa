	  function fnSpecialWithSpace(Ptxtboxval,ptxtName,Result)
	{
		
		var mcontnm,mlencontnm,mctrchar,flag;
		mcontnm=Ptxtboxval;
		mlencontnm=mcontnm.length;
		if(mlencontnm >0)
		{
	    	for(mctrchar=0;mctrchar<=mlencontnm - 1;mctrchar++)
			{
		    	if ((mcontnm.charAt(mctrchar)>='!'&& mcontnm.charAt(mctrchar)<='/')||(mcontnm.charAt(mctrchar)>=':'&& mcontnm.charAt(mctrchar)<='@')||(mcontnm.charAt(mctrchar)>='['&& mcontnm.charAt(mctrchar)<='^')||(mcontnm.charAt(mctrchar)>='_'&& mcontnm.charAt(mctrchar)<='`')||(mcontnm.charAt(mctrchar)>='{'&& mcontnm.charAt(mctrchar)<='~') )
				{
					
					Result.value='Please Remove Special Character(s) From '+ptxtName+'.';
					flag=0;
					break;
				}
				else
				{
				flag=1;
				Result.value='';
				}
				
			}
		}
		else
		{
	
          Result.value=' Please Enter ' + ptxtName+'.';
          flag=0;
		}
		return flag;
	}
	
	
	
	
	    function fnNoSpaceSpecialFirstChar(Ptxtboxval,ptxtName,Result)
	{
	    var num=0;
		var mcontnm,mlencontnm,mctrchar,flag;
		mcontnm=Ptxtboxval;
		mlencontnm=mcontnm.length;
		if(mlencontnm >0)
		{		
		    if (mcontnm.charAt(0) >= '0' && mcontnm.charAt(0) <= '9')
			    {
				    Result.value=ptxtName +' Cannot Start With Number.';
					flag=0;					
					return flag;
			    }
			    else
			    {
	    	        for(mctrchar=0;mctrchar<=mlencontnm - 1;mctrchar++)
			        {
		    	        if (mcontnm.charAt(mctrchar)==' ' && (mctrchar!=0) || mcontnm.charAt(0)==' ')
				        {
        					
					        Result.value='Please Remove Space From '+ptxtName+'.';
					        flag=0;
					        break;
				        }
				        else if ((mcontnm.charAt(mctrchar)>='!'&& mcontnm.charAt(mctrchar)<='/')||(mcontnm.charAt(mctrchar)>=':'&& mcontnm.charAt(mctrchar)<='@')||(mcontnm.charAt(mctrchar)>='['&& mcontnm.charAt(mctrchar)<='^')||(mcontnm.charAt(mctrchar)>='_'&& mcontnm.charAt(mctrchar)<='`')||(mcontnm.charAt(mctrchar)>='{'&& mcontnm.charAt(mctrchar)<='~') )
				        {
        					
					        Result.value='Please Remove Special Character(s) From '+ptxtName+'.';
					        flag=0;
					        break;
				        }
				        else   if (mcontnm.charAt(mctrchar) >= '0' && mcontnm.charAt(mctrchar) <= '9')
			            {				            
					      num=num+1; 
			            }
				        else
				        {
				        flag=1;
				        Result.value='';
				        }        				
			        }			        	        
			    }
		}
		else
		{
	
          Result.value=' Please Enter ' + ptxtName+'.';
          flag=0;
		}
		
		if(num==0) 
		{
		flag=0;
		Result.value=  ptxtName+' Should Contain Alphabet Followed By Numbers. ';
		}
		
		return flag;
	}
	
	
	
			    
			    function fnNoSpaceSpecial(Ptxtboxval,ptxtName,Result)
	{
		
		var mcontnm,mlencontnm,mctrchar,flag;
		mcontnm=Ptxtboxval;
		mlencontnm=mcontnm.length;
		if(mlencontnm >0)
		{
	    	for(mctrchar=0;mctrchar<=mlencontnm - 1;mctrchar++)
			{
		    	if (mcontnm.charAt(mctrchar)==' ' && (mctrchar!=0) || mcontnm.charAt(0)==' ')
				{
					
					Result.value='Please Remove Space From '+ptxtName+'.';
					flag=0;
					break;
				}
				else if ((mcontnm.charAt(mctrchar)>='!'&& mcontnm.charAt(mctrchar)<='/')||(mcontnm.charAt(mctrchar)>=':'&& mcontnm.charAt(mctrchar)<='@')||(mcontnm.charAt(mctrchar)>='['&& mcontnm.charAt(mctrchar)<='^')||(mcontnm.charAt(mctrchar)>='_'&& mcontnm.charAt(mctrchar)<='`')||(mcontnm.charAt(mctrchar)>='{'&& mcontnm.charAt(mctrchar)<='~') )
				{
					
					Result.value='Please Remove Special Character(s) From '+ptxtName+'.';
					flag=0;
					break;
				}
				else
				{
				flag=1;
				Result.value='';
				}
				
			}
		}
		else
		{
	
          Result.value=' Please Enter ' + ptxtName+'.';
          flag=0;
		}
		return flag;
	}
    
      function fnNoPercentLessGreaterSingleQuoteSpecial(Ptxtboxval,ptxtName,Result)
	{
		
		var mcontnm,mlencontnm,mctrchar,flag;
		mcontnm=Ptxtboxval;
		mlencontnm=mcontnm.length;
		if(mlencontnm >0)
		{
	    	for(mctrchar=0;mctrchar<=mlencontnm - 1;mctrchar++)
			{
		        if (mcontnm.charAt(mctrchar)== "'" || mcontnm.charAt(mctrchar)=='<'||mcontnm.charAt(mctrchar)=='>'|| mcontnm.charAt(mctrchar)=='%')
				{					
					Result.value='Please Remove Special Character(s) Single-Quote,<,>,% From '+ptxtName+'.';
					flag=0;
					break;
				}
				else
				{
				flag=1;
				Result.value='';
				}
				
			}
		}
		else
		{
	
          Result.value=' Please Enter ' + ptxtName+'.';
          flag=0;
		}
		return flag;
	}
    
    
    function fnNoBlank(Ptxtboxval,ptxtboxnm,Result)
    {
        var mcontnm,flag;
        mcontnm = Ptxtboxval.length;
        if (mcontnm==0)
        {   
            flag =0;
            Result.value='Please Enter ' + ptxtboxnm +'.';
        }
        else
        {
             flag =1;
            
        }
        return  flag;
    }
    
	function fnNoNumeric(Ptxtboxval,ptxtboxnm,Result)
	{
		
		var mcontnm,mlencontnm,mctrchar,mtxtboxnm,flag;
		mcontnm=Ptxtboxval;
		mtxtboxnm=ptxtboxnm;
		mlencontnm=mcontnm.length;
		for(mctrchar=0;mctrchar<=mlencontnm - 1;mctrchar++)
			{
				if (mcontnm.charAt(mctrchar)==' ' ||mcontnm.charAt(mctrchar)=='.' || (mcontnm.charAt(mctrchar)>='A' && mcontnm.charAt(mctrchar)<='Z')|| (mcontnm.charAt(mctrchar)>='a' && mcontnm.charAt(mctrchar)<='z'))
				{
				 flag =1;
					
				}
				else
				{	
					Result.value=' Please Remove Number(s)/Sp.Chars From '+ ptxtboxnm + '.';
					 flag =0;
					break;
				}
			}
		
		return flag;
	}
	
	
	
	function fnNoNumAllowSpec(Ptxtboxval,ptxtboxnm,Result)
	{
		
		var mcontnm,mlencontnm,mctrchar,mtxtboxnm,flag;
		mcontnm=Ptxtboxval;
		mtxtboxnm=ptxtboxnm;
		mlencontnm=mcontnm.length;
		for(mctrchar=0;mctrchar<=mlencontnm - 1;mctrchar++)
			{
				if (mcontnm.charAt(mctrchar)==' ' || (mcontnm.charAt(mctrchar)>='A' && mcontnm.charAt(mctrchar)<='Z')|| (mcontnm.charAt(mctrchar)>='a' && mcontnm.charAt(mctrchar)<='z'))
				{
					 flag =1;
					
				}
				else if((mcontnm.charAt(mctrchar)>='!'&& mcontnm.charAt(mctrchar)<='/')||(mcontnm.charAt(mctrchar)>=':'&& mcontnm.charAt(mctrchar)<='@')||(mcontnm.charAt(mctrchar)>='['&& mcontnm.charAt(mctrchar)<='^')||(mcontnm.charAt(mctrchar)>='_'&& mcontnm.charAt(mctrchar)<='`')||(mcontnm.charAt(mctrchar)>='{'&& mcontnm.charAt(mctrchar)<='~') )
				{				 
				   flag =1;   				    
				}				
				else
				{		
					    Result.value=' Please Remove Number(s) From '+ ptxtboxnm +   '.';
					     flag =0;
					    break;
					
				}
			}
		
		return  flag;
	}
	
	function fnNoSpecialChar(Ptxtboxval,ptxtName,Result)
	{
		
		var mcontnm,mlencontnm,mctrchar,flag;
		mcontnm=Ptxtboxval;
		mlencontnm=mcontnm.length;
		if(mlencontnm >0)
		{
		for(mctrchar=0;mctrchar<=mlencontnm - 1;mctrchar++)
			{
				
				if ((mcontnm.charAt(mctrchar)>='!'&& mcontnm.charAt(mctrchar)<='/')||(mcontnm.charAt(mctrchar)>=':'&& mcontnm.charAt(mctrchar)<='@')||(mcontnm.charAt(mctrchar)>='['&& mcontnm.charAt(mctrchar)<='^')||(mcontnm.charAt(mctrchar)>='_'&& mcontnm.charAt(mctrchar)<='`')||(mcontnm.charAt(mctrchar)>='{'&& mcontnm.charAt(mctrchar)<='~') )
				{
					
					Result.value='Please Remove Special Character(s) From '+ptxtName+'.';
					 flag =0;
					break;
				}
				else  flag =1;
		
			}
		}
		else
		{
		    
            Result.value=' Please Enter ' + ptxtName + '.';
             flag =0;
		}
		return  flag;
	}
	
  function fnNoSpecialCharAllowPercentage(Ptxtboxval,ptxtName,Result)
	{
		
		var mcontnm,mlencontnm,mctrchar,flag;
		mcontnm=Ptxtboxval;
		mlencontnm=mcontnm.length;
		if(mlencontnm >0)
		{
		for(mctrchar=0;mctrchar<=mlencontnm - 1;mctrchar++)
			{
				if((mcontnm.charAt(mctrchar)!='%'))
				{
				if ((mcontnm.charAt(mctrchar)>='!'&& mcontnm.charAt(mctrchar)<='/')||(mcontnm.charAt(mctrchar)>=':'&& mcontnm.charAt(mctrchar)<='@')||(mcontnm.charAt(mctrchar)>='['&& mcontnm.charAt(mctrchar)<='^')||(mcontnm.charAt(mctrchar)>='_'&& mcontnm.charAt(mctrchar)<='`')||(mcontnm.charAt(mctrchar)>='{'&& mcontnm.charAt(mctrchar)<='~') )
				{
					
					Result.value='Please Remove Special Character(s) From '+ptxtName+'.';
					 flag =0;
					break;
				}
				else  flag =1;
		}
		else
		{
		flag =1;
		}
			}
		}
		else
		{
		    
            Result.value=' Please Enter ' + ptxtName + '.';
             flag =0;
		}
		return  flag;
	} 

    function fnNoChar(Ptxtboxval,ptxtName,Result)
    {
	    var mcontnm,mlencontnm,mctrchar,flag=0;
	    mcontnm=Ptxtboxval;
	    mlencontnm=mcontnm.length;
	    for(mctrchar=0;mctrchar<=mlencontnm - 1;mctrchar++)
		    {
			    if (mcontnm.charAt(mctrchar) >= '0' && mcontnm.charAt(mctrchar) <= '9')
			    {
				    flag =1;
			    }
			    else
			    {
			        Result.value= 'Please Enter Only Number For '+ ptxtName + '.';
			         flag =0;
				    break;
			    }
		    }
		    if(mcontnm.length==0)
		    {
		     Result.value= 'Please Enter  Value For '+ ptxtName + '.';
		      flag =0;
		    }
	    return  flag;
    }
	
	   function fnNoCharWithDot(Ptxtboxval,ptxtName,Result)
    {
	    var mcontnm,mlencontnm,mctrchar,flag=0;
	    mcontnm=Ptxtboxval;
	    mlencontnm=mcontnm.length;
	    for(mctrchar=0;mctrchar<=mlencontnm - 1;mctrchar++)
		    {
			    if ((mcontnm.charAt(mctrchar) >= '0' && mcontnm.charAt(mctrchar) <= '9') ||( mcontnm.charAt(mctrchar) <= '.' ))
			    {
				    flag =1;
			    }
			    else
			    {
			        Result.value= 'Please Enter Only Number For '+ ptxtName + '.';
			         flag =0;
				    break;
			    }
		    }
		    if(mcontnm.length==0)
		    {
		     Result.value= 'Please Enter  Value For '+ ptxtName + '.';
		      flag =0;
		    }
	    return  flag;
    }
	
    function fnNoCharAllowBlank(Ptxtboxval,ptxtName,Result)
    {
	    var mcontnm,mlencontnm,mctrchar,flag=0;
	    mcontnm=Ptxtboxval;
	    mlencontnm=mcontnm.length;
	    for(mctrchar=0;mctrchar<=mlencontnm - 1;mctrchar++)
		    {
			    if (mcontnm.charAt(mctrchar) >= '0' && mcontnm.charAt(mctrchar) <= '9')
			    {
				    flag =1;
			    }
			    else
			    {
			        Result.value= 'Please Enter Only Number For '+ ptxtName + '.';
			         flag =0;
				    break;
			    }
		    }
	    return  flag;
    }
	
	
	function fnDecimalWithComa(Ptxtboxval,ptxtboxnm,Result)
	{
		var mcontnm,mlencontnm,mctrchar,mtxtboxnm,ctr,flag;
		mcontnm=Ptxtboxval;
		mtxtboxnm=ptxtboxnm;
		
		mlencontnm=mcontnm.length;
		chkrepeat=0;
		chkDecnum=0;
		chkDecpnt=0;
	
        for(mctrchar=0;mctrchar<=mlencontnm-1;mctrchar++)
        {
            if (mcontnm.charAt(mctrchar) >= '0' && mcontnm.charAt(mctrchar) <= '9'   ||  mcontnm.charAt(mctrchar) == ','  )
            {
                 flag =1;
            }
              else if (mcontnm.charAt(mctrchar) != '.')
              {
                Result.value=mtxtboxnm + ' Can Have Only Numeric Value .';  
                 flag =0;
                   break;
              }
            
            else if (mcontnm.charAt(mctrchar) == '.')
            {
                chkrepeat=chkrepeat + 1;
                chkDecnum=chkDecnum+1;
                if(chkrepeat> 1)
                {
                    Result.value=mtxtboxnm + ' Can Have Only One Decimal Point.';  
                     flag =0;
                    break;
                }					
                else
                {
                    flag =1;
                }
            }
            else if(mcontnm.charAt(mctrchar) >= '0' && mcontnm.charAt(mctrchar) <= '9' || (chkDecnum!=1))
            {
                chkDecpnt=chkDecpnt+1;
                if(chkDecpnt>3)
                {
                    Result.value=mtxtboxnm + ' Can Have Only Three Decimal Digits.';  
                    flag =0;
                    break;
                }
                else
                {
                    flag =1;
                }
            }
            else
            {
                Result.value=mtxtboxnm + ' Should Have Only Numeric Data.';  
                flag =0;
                break;
            }
        }	
	
	return  flag ;
}

    function fnDecimalWithThreeDigits(Ptxtboxval,ptxtboxnm,Result)
	{
		var mcontnm,mlencontnm,mctrchar,mtxtboxnm,ctr,flag;
		mcontnm=Ptxtboxval;
		mtxtboxnm=ptxtboxnm;
		
		mlencontnm=mcontnm.length;
		chkrepeat=0;
		chkDecnum=0;
		chkDecpnt=0;
		
        for(mctrchar=0;mctrchar<=mlencontnm-1;mctrchar++)
        {
//            if (mcontnm.charAt(mctrchar) >= '0' && mcontnm.charAt(mctrchar) <= '9' && (chkDecnum!=1) ||  mcontnm.charAt(mctrchar) == ','  )
            if (mcontnm.charAt(mctrchar) >= '0' && mcontnm.charAt(mctrchar) <= '9')
            {
                 flag =1;
            }
              else if (mcontnm.charAt(mctrchar) != '.')
              {
                Result.value=mtxtboxnm + ' Can Have Only Numeric Value .'; 
                 flag =0; 
                  break;
              }
            
            
            else if (mcontnm.charAt(mctrchar) == '.')
            {
                chkrepeat=chkrepeat + 1;
                chkDecnum=chkDecnum+1;
                if(chkrepeat> 1)
                {
                    Result.value=mtxtboxnm + ' Can Have Only One Decimal Point.';  
                     flag =0;
                    break;
                }					
                else
                {
                     flag =1;
                }
            }
            else if(mcontnm.charAt(mctrchar) >= '0' && mcontnm.charAt(mctrchar) <= '9' || (chkDecnum!=1))
            {
                chkDecpnt=chkDecpnt+1;
                if(chkDecpnt>2)
                {
                    Result.value=mtxtboxnm + ' Can Have Only Two Decimal Digits.';  
                    flag =0;
                    break;
                }
                else
                {
                    flag =1;
                }
            }
            else
            {
                Result.value=mtxtboxnm + ' Should Have Only Numeric Data.';  
                flag =0;
                break;
            }
            if(mctrchar==3)
            {
                if (mcontnm.charAt(0) != '.')
                if (mcontnm.charAt(1) != '.')
                if (mcontnm.charAt(2) != '.')
                if (mcontnm.charAt(3) != '.')
                  {
                    Result.value=mtxtboxnm + ' Can Have Only three digits before decimal point .'; 
                     flag =0; 
                      break;
                  }
            }
        }	
	
	return  flag;
}
	
	
	function fnDecimal(Ptxtboxval,ptxtboxnm,Result)
	{
		var mcontnm,mlencontnm,mctrchar,mtxtboxnm,ctr,flag;
		mcontnm=Ptxtboxval;
		mtxtboxnm=ptxtboxnm;
		
		mlencontnm=mcontnm.length;
		chkrepeat=0;
		chkDecnum=0;
		chkDecpnt=0;
		
        for(mctrchar=0;mctrchar<=mlencontnm-1;mctrchar++)
        {
//            if (mcontnm.charAt(mctrchar) >= '0' && mcontnm.charAt(mctrchar) <= '9' && (chkDecnum!=1) ||  mcontnm.charAt(mctrchar) == ','  )
if (mcontnm.charAt(mctrchar) >= '0' && mcontnm.charAt(mctrchar) <= '9')
            {
                 flag =1;
            }
             else if(mcontnm.charAt(mctrchar) =='-' && mctrchar==0)
            {             
                    flag =1;
                
            }
              else if (mcontnm.charAt(mctrchar) != '.')
              {
                Result.value=mtxtboxnm + ' Can Have Only Numeric Value .'; 
                 flag =0; 
                  break;
              }
            
            else if (mcontnm.charAt(mctrchar) == '.')
            {
                chkrepeat=chkrepeat + 1;
                chkDecnum=chkDecnum+1;
                if(chkrepeat> 1)
                {
                    Result.value=mtxtboxnm + ' Can Have Only One Decimal Point.';  
                     flag =0;
                    break;
                }					
                else
                {
                     flag =1;
                }
            }
            else if(mcontnm.charAt(mctrchar) >= '0' && mcontnm.charAt(mctrchar) <= '9' || (chkDecnum!=1))
            {
                chkDecpnt=chkDecpnt+1;
                if(chkDecpnt>3)
                {
                    Result.value=mtxtboxnm + ' Can Have Only Three Decimal Digits.';  
                    flag =0;
                    break;
                }
                else
                {
                    flag =1;
                }
            }
            
            
            else
            {
                Result.value=mtxtboxnm + ' Should Have Only Numeric Data.';  
                flag =0;
                break;
            }
        }	
	
	return  flag;
}


function fnComboSelect(Ptxtboxval,ptxtName,Result)
{
    var mConVal,PtxtboxVal,mConLen,flag;
    mConVal = Ptxtboxval;
   	if(mConVal != '0' && mConVal != '-1' && mConVal != '')
    {
         flag =1;
        return flag;
    }
    else
    {
        flag=0;        
       Result.value= 'Please Select ' + ptxtName + '.';
         return flag;
    }
}



function fnNoHTMLChar(Ptxtboxval,ptxtName,Result)
{
    var flag=0;
	mcontnm=Ptxtboxval;
	mlencontnm=mcontnm.length;
	for(mctrchar=0;mctrchar<=mlencontnm - 1;mctrchar++)
	{
	   if (mcontnm.charAt(mctrchar) == '<')
	   {
	    if((mctrchar + 1) <= mlencontnm - 1)
	    {
	      if(mcontnm.charAt(mctrchar+1) == '%')
	      {
	       flag =1;
	       break;
	      }
	    }
	   }

	   if (mcontnm.charAt(mctrchar) == '%')
	   {
	    if((mctrchar + 1) <= mlencontnm - 1)
	    {
	      if(mcontnm.charAt(mctrchar+1) == '>')
	      {
	       flag =1;
	       break;
	      }
	    }
	   }

    }
    
    if(flag == 1)
    Result.value= ptxtName + " Cannot Contain < With % Or % With >.";
    
    return  flag;
}
//Function Name : fnCheckLength
//Purpose :This function makes a Checks Length
//Added By Subahani SM on 11 Mar 2010
function fnCheckLength(Ptxtboxval,Ptxtboxnm,PtxtLength,Result) {
    var mcontnm, flag;
    mcontnm = Ptxtboxval.length;    
    if (mcontnm > PtxtLength) {        
        flag = 0;
        Result.value = Ptxtboxnm + ' Accepts Maximum ' + PtxtLength + ' Characters .';
    }
    else {
        flag = 1;

    }
    return flag;
}


function blockNonNumbers(obj, e, allowDecimal, allowNegative) {
    var key;
    var isCtrl = false;
    var keychar;
    var reg;
    
    if (window.event) {
        key = e.keyCode;
        isCtrl = window.event.ctrlKey
    }
    else if (e.which) {
        key = e.which;
        isCtrl = e.ctrlKey;
    }

    if (isNaN(key)) return true;

    keychar = String.fromCharCode(key);

    // check for backspace or delete, or if Ctrl was pressed
    if (key == 8 || isCtrl) {
        return true;
    }

    reg = /\d/;
    var isFirstN = allowNegative ? keychar == '-' && obj.value.indexOf('-') == -1 : false;
    var isFirstD = allowDecimal ? keychar == '.' && obj.value.indexOf('.') == -1 : false;

    return isFirstN || isFirstD || reg.test(keychar);
}
function val_Numeric(Ptxtboxval,ptxtboxnm,Result)
	{
		var mcontnm,mlencontnm,mctrchar,flag,mtxtboxnm;
		mcontnm=Ptxtboxval;
		mtxtboxnm=ptxtboxnm;
		mlencontnm=mcontnm.length;
		for(mctrchar=0;mctrchar<=mlencontnm - 1;mctrchar++)
			{
				if (mcontnm.charAt(mctrchar) >= '0' && mcontnm.charAt(mctrchar) <= '9')
				{
					flag=1;
				}
				else
				{
					flag=0;
					Result.value = Ptxtboxnm + ' Accepts Only Numeric.';
					break;
				}
			}
		return flag;
}
function DateDiffVal(fromdate, todate, CustDateFormat, pftxtboxnm, pttxtboxnm, Vtype, Dtype, dj) {
    var mfdate, mtdate, flag, mfday, mfmon, mfyr, mtyr, mtmon, mtday, frdt, todt, vtype, dtype, dtoj
    vtype = Vtype;
    dtype = Dtype;
    dtoj = dj;
    mfdate = fromdate;
    mtdate = todate;
    mfday = "", mfmon = "", mfyr = "", mtyr = "", mtmon = "", mtday = ""
    if (mfdate == '' || mtdate == '') {
        flag = 0;
    }
    else {
        flag = 1;
        if (flag == 1) {
            //alert(fromdate+"*"+todate+"*"+CustDateFormat+"*"+pftxtboxnm+"*"+pttxtboxnm+"*"+Vtype+"*"+Dtype+"*"+dj)

            for (mctrchar = 0; mctrchar <= CustDateFormat.length - 1 && flag != 0; mctrchar++) {
                switch (CustDateFormat.charAt(mctrchar)) {
                    case 'D':

                        mfday = mfday + mfdate.charAt(mctrchar)
                        mtday = mtday + mtdate.charAt(mctrchar)
                        break;

                    case 'M':

                        mfmon = mfmon + mfdate.charAt(mctrchar)
                        mtmon = mtmon + mtdate.charAt(mctrchar)
                        break;

                    case 'Y':

                        mfyr = mfyr + mfdate.charAt(mctrchar)
                        mtyr = mtyr + mtdate.charAt(mctrchar)
                        break;
                    case '/':

                        if (mfdate.charAt(mctrchar) != '/' || mtdate.charAt(mctrchar) != '/')
                            flag = 0;
                        break;

                }
            }



            /*
            fslash = mfdate.indexOf("-",0);
            mfyr = mfdate.substring(0,(fslash));
            sslash = mfdate.indexOf("-",(fslash+1));
            mfmon = mfdate.substring((fslash+1),sslash);
            mfday = mfdate.substring((sslash+1),mfdate.length);
			
			alert(mfyr);
            alert(mfmon);
            alert(mfday);
																			
            fslash = mtdate.indexOf("-",0);
            mtyr = mtdate.substring(0,(fslash));
            sslash = mtdate.indexOf("-",(fslash+1));
            mtmon = mtdate.substring((fslash+1),sslash);
            mtday = mtdate.substring((sslash+1),mtdate.length);
            alert(mtday);
            alert(mtmon);
            alert(mtyr);
            */

            if (parseInt(mfyr, 10) > parseInt(mtyr, 10)) {
                flag = 0;

            }
            else if (parseInt(mfyr, 10) < parseInt(mtyr, 10)) {
                flag = 1;

            }
            else if (parseInt(mfmon, 10) > parseInt(mtmon, 10)) {
                flag = 0;

            }
            else if (parseInt(mfmon, 10) < parseInt(mtmon, 10)) {
                flag = 1;

            }
            else if (dtype != "G") {
                if (parseInt(mfday, 10) > parseInt(mtday, 10)) {
                    flag = 0;
                }

            }
            else if (dtype == "G") {
                if (parseInt(mfday, 10) >= parseInt(mtday, 10)) {
                    flag = 0;
                }

            }
            else if (parseInt(mfday, 10) < parseInt(mtday, 10)) {
                flag = 1;

            }
            else {
                flag = 1;

            }

        }

        if (flag == 0) {
            if (dtoj != "") {
                if (vtype == "C") {
                    alert("Invalid " + pftxtboxnm + "." + '\n' + "Date Entered should be greater than or equal to the " + pttxtboxnm + " (" + dtoj + " )");
                }
                else if (vtype == "CA") {
                    alert("Invalid " + pftxtboxnm + "." + '\n' + "Date Entered should be greater than the " + pttxtboxnm + " (" + dtoj + " )");
                }
                else if (vtype == "L") {
                    alert("Invalid " + pftxtboxnm + "." + '\n' + "Date Entered should be less than the " + pttxtboxnm + " (" + dtoj + " )");
                }
                else {
                    alert("Invalid " + pftxtboxnm + "." + '\n' + "Date Entered should be less than or equal to " + pttxtboxnm + " (" + dtoj + " )");
                }
            }
            else {
                if (vtype == "C") {
                    alert("Invalid " + pftxtboxnm + "." + '\n' + "Date Entered should be greater than the " + pttxtboxnm);
                }
                else if (vtype == "G") {
                    alert("Invalid " + pftxtboxnm + "." + '\n' + "Date Entered should be greater than or equal to the " + pttxtboxnm);
                }
                else if (vtype == "L") {
                    alert("Invalid " + pftxtboxnm + "." + '\n' + "Date Entered should be less than the " + pttxtboxnm);
                }
                else {
                    alert("Invalid " + pftxtboxnm + "." + '\n' + "Date Entered should be less than or equal to " + pttxtboxnm);
                }
            }
        }
        return flag;
    }
}

function fnEMailValidate(StrMailadd) {
    var strEMail;
    strEMail = StrMailadd;
    ok = true;

    if (strEMail == "") {
        //alert("Please enter e-mail address.")
        //frmPractice.txtEMail.focus()
        strEMail = StrMailadd;
        ok = false;
    }
    else {
        //strEMail=StrMailadd;
        strEMail = StrMailadd;
        at_pos = strEMail.indexOf("@");
        dot_pos = strEMail.indexOf(".");
        if (at_pos < 1 || dot_pos < 1) {
            alert("Please check position of '@' and '.' in email address.");
            //frmPractice.txtEMail.focus();
            ok = false;
        }
        else {
            //strEMail=StrMailadd;
            strEMail = StrMailadd;
            condition = "yes";
            var at_count = 0;
            var dot_count = 0;
            var temp = 0;
            for (var i = 0; i < strEMail.length; i++) {
                if ((strEMail.charCodeAt(i) > 0 && strEMail.charCodeAt(i) < 45) || (strEMail.charCodeAt(i) > 45 && strEMail.charCodeAt(i) < 48) || (strEMail.charCodeAt(i) > 57 && strEMail.charCodeAt(i) < 65) || (strEMail.charCodeAt(i) > 91 && strEMail.charCodeAt(i) < 95) || strEMail.charCodeAt(i) > 122) {
                    if (strEMail.charAt(i) == "@" || strEMail.charAt(i) == ".") {
                        if (strEMail.charAt(i) == "@") { at_count++ } else { dot_count++ } // counts the no. of times @ and . appears in email
                        if (dot_count >= 1) {
                            dot_pos = i;
                            if ((dot_pos > at_pos) && temp == 0) {
                                pos = dot_pos - at_pos;
                                temp++;
                            }
                        }
                    }
                    else {
                        condition = "no";
                        i = strEMail.length;
                    }
                }
            }
            if (condition == "no") {
                alert("Your email contains a blank space or special character.");
                //frmPractice.txtEMail.focus();
                ok = false;
            }
            else {
                if (at_count > 1) {
                    alert("E-mail contains extra @ ");
                    //frmPractice.txtEMail.focus();
                    ok = false;
                }
                else {
                    if (pos < 2) {
                        alert("Missing domain name between '@' and '.'");
                        //frmPractice.txtEMail.focus();
                        ok = false;
                        i = strEMail.length;
                    }
                    else {
                        count = dot_pos + 1;
                        domain = "";
                        for (count; count < strEMail.length; count++) {
                            domain = domain + strEMail.charAt(count);
                        }
                        dom = new Array("au", "uk", "com", "net", "org", "edu", "in", "mil", "gov", "arpa", "biz", "aero", "name", "coop", "info", "pro", "museum");
                        error = "yes";
                        for (var k = 0; k < dom.length; k++) {
                            if (domain.toUpperCase == dom[k].toUpperCase) {
                                k = dom.length;
                                error = "no";
                            }
                        }
                        if ((error == "yes" && (domain.length > 2)) || (domain.length < 2)) {
                            alert("Domain name must end with well known domains \n or 2-lettered country names. eg com,edu,in etc.");
                            //frmPractice.txtEMail.focus();
                            ok = false;
                        }
                    }
                }
            }
        }
    }
    at_pos = strEMail.indexOf("@");
    dot_pos = strEMail.indexOf(".");
    if (ok != false) {
        if ((at_pos - dot_pos == 1) || (at_pos - dot_pos == -1)) {
            alert("E-Mail address can not have continuous . and @ characters.");
            ok = false;

        }
    }
    return ok;
}

