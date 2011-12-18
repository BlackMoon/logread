// JScript File

function Sign( text )
{
 var cert;
 var SignedData;
 var Signer;
 var retVal=null;

  cert = getCertificate();
  if(cert==null)
    return null;

  try
  {
    SignedData = new ActiveXObject("CAPICOM.SignedData");
    Signer     = new ActiveXObject("CAPICOM.Signer");
  }  
  catch (e)
  {
    alert("CAPICOM.SignedData.\nВыполнение CAPICOM.SignedData вызвало ошибку (неправильно установлен CAPICOM?)\n" + e.description+" [0x"+toHex(e.number)+"]");
    return null;
  }

  try
  {
    SignedData.Content = text;
    Signer.Certificate = cert;
    retVal = SignedData.Sign(Signer,true,0);
  }
  catch (e)
  {
    alert("CAPICOM.SignedData.\nНевозможно подписать ЭЦП\n" + e.description+" [0x"+toHex(e.number)+"]");
    return null;
  }
  return retVal;
}

function CoSign( text, sign )
{
 var cert;
 var SignedData;
 var Signer;
 var retVal=null;

  cert = getCertificate();
  if(cert==null)
    return null;

  try
  {
    SignedData = new ActiveXObject("CAPICOM.SignedData");
    Signer     = new ActiveXObject("CAPICOM.Signer");
  }  
  catch (e)
  {
    alert("CAPICOM.SignedData.\nВыполнение CAPICOM.SignedData вызвало ошибку (неправильно установлен CAPICOM?)\n" + e.description+" [0x"+toHex(e.number)+"]");
    return null;
  }

  try
  {
    SignedData.Content = text;
    Signer.Certificate = cert;
    SignedData.Verify( sign, true, 0 );
    retVal = SignedData.CoSign( Signer, true );
  }
  catch (e)
  {
    alert("CAPICOM.SignedData.\nНевозможно соподписать ЭЦП\n" + e.description+" [0x"+toHex(e.number)+"]");
    return null;
  }
  return retVal;
}

function getCertificate()
{
 var cert = null;

  try
  {
    var store = new ActiveXObject("CAPICOM.Store");

  }
  catch (e)
  {
    alert("CAPICOM.Store.\nВыполнение CAPICOM.Store вызвало ошибку (неправильно установлен CAPICOM?)\n" + e.description+" [0x"+toHex(e.number)+"]");
  }

  try
  {
    store.Open(2, "My", 0);
    var Certificates = store.Certificates.Select("Выберите ключа для ЭЦП", "Выбор ключа", false);
    Certificates = Certificates.Find(6, 2);
    if( Certificates.Count > 0 )
    {
      cert = Certificates.Item(1);
    }
    else
    {
      alert("CAPICOM.Store.\nНет сертификатов с закрытым ключом для подписи")
    }
    store.Close();
  }
  catch (e)
  {
    cert = null;
    alert("CAPICOM.Store.\nНевозможно выбрать сертификат для подписи\n" + e.description+" [0x"+toHex(e.number)+"]");
  }

  return cert;
}

function toHex(n)
{
 var hex=new Array(0,1,2,3,4,5,6,7,8,9,"A","B","C","D","E","F");
  return(""+hex[Math.abs((0xf0000000&n)>>28)]+hex[(0x0f000000&n)>>24]+hex[(0x00f00000&n)>>20]+hex[(0x000f0000&n)>>16]+hex[(0x0000f000&n)>>12]+hex[(0x00000f00&n)>> 8]+hex[(0x000000f0&n)>> 4]+hex[(0x0000000f&n)>> 0]);
}