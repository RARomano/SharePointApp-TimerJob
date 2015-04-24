# SharePointApp-TimerJob

Esse exemplo mostra como criar um TimerJob utilizando SharePoint apps.

## Rodando esse projeto

Para rodar esse exemplo você precisará:
- Visual Studio 2013
- SharePoint 2013 / SharePoint Online

### 1 - Clonar ou fazer o download do Repositório

Rode o comando abaixo no Git Shell:

`git clone https://github.com/RARomano/SharePointApp-TimerJob.git`

### 2 - Registrar um novo App

Abrir a URL **"_layouts/AppRegNew.aspx"** no seu SharePoint 

Clicar no botão gerar do ID do Cliente e do Segredo do Cliente. Digitar o Título da App preencher um domínio para a APP (pode ser localhost) e uma URL de redirecionamento (pode ser a URL do seu SharePoint)

![Criar um App](http://rodrigoromano.net/wp-content/uploads/2015/03/4.jpg)

### 3 - Alterar o arquivo App.Config

![App.Config](http://rodrigoromano.net/wp-content/uploads/2015/03/5.jpg)

### 4 - Dar permissões ao App

Abrir a URL **“_layouts/AppInv.aspx”** no seu SharePoint e digite o ClientID criado na etapa 2.

![Permissões](http://rodrigoromano.net/wp-content/uploads/2015/03/6.jpg)

No campo XML de Solicitação de Permissão do Aplicativo cole o XML abaixo:

```XML
<AppPermissionRequests AllowAppOnlyPolicy="true">
    <AppPermissionRequest Scope="http://sharepoint/content/tenant" Right="Manage" />
</AppPermissionRequests>
```

Com esse XML, você dará permissão em todo o Tenant para o App. Altere se for necessário.

### 5 - Alterar o código

Alterar a Url e incluir a funcionalidade desejada.

```C#
string siteUrl = "http://localhost";
Uri uri = new Uri(siteUrl);

string realm = TokenHelper.GetRealmFromTargetUrl(uri);

//Get the access token for the URL.  
//   Requires this app to be registered with the tenant
string accessToken = TokenHelper.GetAppOnlyAccessToken(
    TokenHelper.SharePointPrincipal,
    uri.Authority, realm).AccessToken;

using (var ctx = TokenHelper.GetClientContextWithAccessToken(uri.ToString(), accessToken))
{
    /// codigo vai aqui
    ctx.Load(ctx.Web);
    var webTitle = ctx.Web.Title;

    ctx.ExecuteQuery();

    Console.Write(webTitle);
}
```
