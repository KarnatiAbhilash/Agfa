// JScript File
//function Trim(sValue)
//{
//	var ichar, icount;
//	ichar = sValue.length - 1;
//	icount = -1;
//	// finding out how spaces at the end
//	while( sValue.charAt(ichar) == ' ' && ichar > icount )
//		--ichar;
//	// if there are spaces then removing them
//	if( ichar != (sValue.length - 1))
//		sValue = sValue.slice(0, ichar+1);
//		
//	ichar = 0;
//	icount = sValue.length - 1;
//	// finding out the spaces at the begging
//	while( sValue.charAt(ichar) == ' ' && ichar < icount )
//		++ichar;
//	// if there are spaces at front then removing them
//	if(ichar != 0 )
//		sValue = sValue.slice(ichar, sValue.length);	
//	return sValue;
//}

String.prototype.trim = function() {	return this.replace(/^\s+|\s+$/g,"");}
String.prototype.ltrim = function() {	return this.replace(/^\s+/,"");}
String.prototype.rtrim = function() {	return this.replace(/\s+$/,"");}
String.prototype.replaceall = function(strOld, strNew) {  var txt = new RegExp(strOld,"g");  return this.replace(txt ,strNew)}

function ChangeDateFormatForSqlServer(strDate,strDateFormat)
{
  if(strDateFormat.toUpperCase() == "DD-MM-YYYY")
  {
    var strDay = strDate.substring(0,strDate.indexOf("-"));
    var strMonth = strDate.substring(strDate.indexOf("-") + 1, strDate.lastIndexOf("-"));
    var strYear = strDate.substring(strDate.lastIndexOf("-") + 1, strDate.length);
    return strMonth + "-" + strDay + "-" + strYear;
  }
  else if(strDateFormat.toUpperCase() == "YYYY-MM-DD")
  {
    var strDay =  strDate.substring(strDate.lastIndexOf("-") + 1, strDate.length);
    var strMonth = strDate.substring(strDate.indexOf("-") + 1, strDate.lastIndexOf("-"));
    var strYear = strDate.substring(0,strDate.indexOf("-"));
    return strMonth + "-" + strDay + "-" + strYear;
  }
  else
  {
    return strDate
  }
}
