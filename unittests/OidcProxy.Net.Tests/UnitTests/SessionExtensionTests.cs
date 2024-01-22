using FluentAssertions;

namespace OidcProxy.Net.Tests.UnitTests;

public class SessionExtensionTests
{
    [InlineData("/")]
    [InlineData("/test.aspx")]
    [InlineData("/hello")]
    [InlineData("/you/may/go/here")]
    [Theory]
    public async Task ShouldStoreLandingPageInSession(string landingPage)
    {
        var testSession = new TestSession();

        await testSession.SetUserPreferredLandingPageAsync(landingPage);
        var actual = testSession.GetUserPreferredLandingPage();

        actual.Should().Be(landingPage);
    }
    
    [Fact]
    public async Task WhenEmptyLandingPage_ShouldRemoveLandingPageValueFromSession()
    {
        var testSession = new TestSession();

        await testSession.SetUserPreferredLandingPageAsync(string.Empty);
        var actual = testSession.GetUserPreferredLandingPage();

        actual.Should().BeNull();
    }
    
    [InlineData("https://www.myhackersite.com/youregonnabepwned.html")]
    [InlineData("http://www.myhackersite.com/youregonnabepwned.html")]
    [InlineData("//www.myhackersite.com/youregonnabepwned.html")]
    [InlineData("https://www.myhackersite.com")]
    [InlineData("zoommtg://zoom.us/join?action=join&confno=123456789&zc=64&confid=dXRpZDmMTZl&browser=chrome'")]
    [InlineData("admin:/usr/var/foo.txt")]
    [InlineData("app://visualstudio")]
    [InlineData("freeplane:/%20yadayada#ID_25")]
    [InlineData("javascript:alert('pwnage')")]
    [InlineData("jdbc:somejdbcvendor:other_data")]
    [InlineData("psns://browse?product=325256")]
    [InlineData("rdar://10198949")]
    [InlineData("s3://bucket/")]
    [InlineData("slack://open?team=owasp")]
    [InlineData("stratum+tcp://server:25")]
    [InlineData("viber://pa?chatURI=xyz")]
    [InlineData("web+lowercasealphabeticalcharacters")]
    [InlineData(@"\\C:\youre\pwned\maliciousexecutable.exe")]
    [InlineData(@"file://page.html")]
    [InlineData(@"//google.com/%2f..")]
    [InlineData(@"//www.whitelisteddomain.tld@google.com/%2f..")]
    [InlineData(@"///google.com/%2f..")]
    [InlineData(@"///www.whitelisteddomain.tld@google.com/%2f..")]
    [InlineData(@"////google.com/%2f..")]
    [InlineData(@"////www.whitelisteddomain.tld@google.com/%2f..")]
    [InlineData(@"https://google.com/%2f..")]
    [InlineData(@"https://www.whitelisteddomain.tld@google.com/%2f..")]
    [InlineData(@"/https://google.com/%2f..")]
    [InlineData(@"/https://www.whitelisteddomain.tld@google.com/%2f..")]
    [InlineData(@"//www.google.com/%2f%2e%2e")]
    [InlineData(@"//www.whitelisteddomain.tld@www.google.com/%2f%2e%2e")]
    [InlineData(@"///www.google.com/%2f%2e%2e")]
    [InlineData(@"///www.whitelisteddomain.tld@www.google.com/%2f%2e%2e")]
    [InlineData(@"////www.google.com/%2f%2e%2e")]
    [InlineData(@"////www.whitelisteddomain.tld@www.google.com/%2f%2e%2e")]
    [InlineData(@"https://www.google.com/%2f%2e%2e")]
    [InlineData(@"https://www.whitelisteddomain.tld@www.google.com/%2f%2e%2e")]
    [InlineData(@"/https://www.google.com/%2f%2e%2e")]
    [InlineData(@"/https://www.whitelisteddomain.tld@www.google.com/%2f%2e%2e")]
    [InlineData(@"//google.com/")]
    [InlineData(@"//www.whitelisteddomain.tld@google.com/")]
    [InlineData(@"///google.com/")]
    [InlineData(@"///www.whitelisteddomain.tld@google.com/")]
    [InlineData(@"////google.com/")]
    [InlineData(@"////www.whitelisteddomain.tld@google.com/")]
    [InlineData(@"https://google.com/")]
    [InlineData(@"https://www.whitelisteddomain.tld@google.com/")]
    [InlineData(@"/https://google.com/")]
    [InlineData(@"/https://www.whitelisteddomain.tld@google.com/")]
    [InlineData(@"//google.com//")]
    [InlineData(@"//www.whitelisteddomain.tld@google.com//")]
    [InlineData(@"///google.com//")]
    [InlineData(@"///www.whitelisteddomain.tld@google.com//")]
    [InlineData(@"////google.com//")]
    [InlineData(@"////www.whitelisteddomain.tld@google.com//")]
    [InlineData(@"https://google.com//")]
    [InlineData(@"https://www.whitelisteddomain.tld@google.com//")]
    [InlineData(@"//https://google.com//")]
    [InlineData(@"//https://www.whitelisteddomain.tld@google.com//")]
    [InlineData(@"//www.google.com/%2e%2e%2f")]
    [InlineData(@"//www.whitelisteddomain.tld@www.google.com/%2e%2e%2f")]
    [InlineData(@"///www.google.com/%2e%2e%2f")]
    [InlineData(@"///www.whitelisteddomain.tld@www.google.com/%2e%2e%2f")]
    [InlineData(@"////www.google.com/%2e%2e%2f")]
    [InlineData(@"////www.whitelisteddomain.tld@www.google.com/%2e%2e%2f")]
    [InlineData(@"https://www.google.com/%2e%2e%2f")]
    [InlineData(@"https://www.whitelisteddomain.tld@www.google.com/%2e%2e%2f")]
    [InlineData(@"//https://www.google.com/%2e%2e%2f")]
    [InlineData(@"//https://www.whitelisteddomain.tld@www.google.com/%2e%2e%2f")]
    [InlineData(@"///www.google.com/%2e%2e")]
    [InlineData(@"///www.whitelisteddomain.tld@www.google.com/%2e%2e")]
    [InlineData(@"////www.google.com/%2e%2e")]
    [InlineData(@"////www.whitelisteddomain.tld@www.google.com/%2e%2e")]
    [InlineData(@"https:///www.google.com/%2e%2e")]
    [InlineData(@"https:///www.whitelisteddomain.tld@www.google.com/%2e%2e")]
    [InlineData(@"//https:///www.google.com/%2e%2e")]
    [InlineData(@"//www.whitelisteddomain.tld@https:///www.google.com/%2e%2e")]
    [InlineData(@"/https://www.google.com/%2e%2e")]
    [InlineData(@"/https://www.whitelisteddomain.tld@www.google.com/%2e%2e")]
    [InlineData(@"///www.google.com/%2f%2e%2e")]
    [InlineData(@"///www.whitelisteddomain.tld@www.google.com/%2f%2e%2e")]
    [InlineData(@"////www.google.com/%2f%2e%2e")]
    [InlineData(@"////www.whitelisteddomain.tld@www.google.com/%2f%2e%2e")]
    [InlineData(@"https:///www.google.com/%2f%2e%2e")]
    [InlineData(@"https:///www.whitelisteddomain.tld@www.google.com/%2f%2e%2e")]
    [InlineData(@"/https://www.google.com/%2f%2e%2e")]
    [InlineData(@"/https://www.whitelisteddomain.tld@www.google.com/%2f%2e%2e")]
    [InlineData(@"/https:///www.google.com/%2f%2e%2e")]
    [InlineData(@"/https:///www.whitelisteddomain.tld@www.google.com/%2f%2e%2e")]
    [InlineData(@"/%09/google.com")]
    [InlineData(@"%09/www.whitelisteddomain.tld@google.com")]
    [InlineData(@"//%09/google.com")]
    [InlineData(@"//%09/www.whitelisteddomain.tld@google.com")]
    [InlineData(@"///%09/google.com")]
    [InlineData(@"///%09/www.whitelisteddomain.tld@google.com")]
    [InlineData(@"////%09/google.com")]
    [InlineData(@"////%09/www.whitelisteddomain.tld@google.com")]
    [InlineData(@"https://%09/google.com")]
    [InlineData(@"https://%09/www.whitelisteddomain.tld@google.com")]
    [InlineData(@"/%5cgoogle.com")]
    [InlineData(@"/%5cwww.whitelisteddomain.tld@google.com")]
    [InlineData(@"//%5cgoogle.com")]
    [InlineData(@"//%5cwww.whitelisteddomain.tld@google.com")]
    [InlineData(@"///%5cgoogle.com")]
    [InlineData(@"///%5cwww.whitelisteddomain.tld@google.com")]
    [InlineData(@"////%5cgoogle.com")]
    [InlineData(@"////%5cwww.whitelisteddomain.tld@google.com")]
    [InlineData(@"https://%5cgoogle.com")]
    [InlineData(@"https://%5cwww.whitelisteddomain.tld@google.com")]
    [InlineData(@"/https://%5cgoogle.com")]
    [InlineData(@"/https://%5cwww.whitelisteddomain.tld@google.com")]
    [InlineData(@"https://google.com")]
    [InlineData(@"https://www.whitelisteddomain.tld@google.com")]
    [InlineData(@"javascript:alert(1);")]
    [InlineData(@"javascript:alert(1)")]
    [InlineData(@"//javascript:alert(1);")]
    [InlineData(@"/javascript:alert(1);")]
    [InlineData(@"//javascript:alert(1)")]
    [InlineData(@"/javascript:alert(1)")]
    [InlineData(@"/%5cjavascript:alert(1);")]
    [InlineData(@"/%5cjavascript:alert(1)")]
    [InlineData(@"//%5cjavascript:alert(1);")]
    [InlineData(@"//%5cjavascript:alert(1)")]
    [InlineData(@"/%09/javascript:alert(1);")]
    [InlineData(@"/%09/javascript:alert(1)")]
    [InlineData(@"javascript%3aalert(1);")]
    [InlineData(@"javascript%3aalert(1)")]
    [InlineData(@"//javascript%3aalert(1);")]
    [InlineData(@"/javascript%3aalert(1);")]
    [InlineData(@"//javascript%3aalert(1)")]
    [InlineData(@"/javascript%3aalert(1)")]
    [InlineData(@"/%5cjavascript%3aalert(1);")]
    [InlineData(@"/%5cjavascript%3aalert(1)")]
    [InlineData(@"//%5cjavascript%3aalert(1);")]
    [InlineData(@"//%5cjavascript%3aalert(1)")]
    [InlineData(@"/%09/javascript%3aalert(1);")]
    [InlineData(@"/%09/javascript%3aalert(1)")]
    [InlineData(@"java%0d%0ascript%0d%0a:alert(0)")]
    [InlineData(@"//google.com")]
    [InlineData(@"https:google.com")]
    [InlineData(@"//google%E3%80%82com")]
    [InlineData(@"\/\/google.com/")]
    [InlineData(@"/\/google.com/")]
    [InlineData(@"//google%00.com")]
    [InlineData(@"https://www.whitelisteddomain.tld/https://www.google.com/")]
    [InlineData(@""";alert(0);//")]
    [InlineData(@"javascript://www.whitelisteddomain.tld?%a0alert%281%29")]
    [InlineData(@"javascript%3a//www.whitelisteddomain.tld?%a0alert%281%29")]
    [InlineData(@"http://0xd8.0x3a.0xd6.0xce")]
    [InlineData(@"http://www.whitelisteddomain.tld@0xd8.0x3a.0xd6.0xce")]
    [InlineData(@"http://3H6k7lIAiqjfNeN@0xd8.0x3a.0xd6.0xce")]
    [InlineData(@"http://XY>.7d8T\205pZM@0xd8.0x3a.0xd6.0xce")]
    [InlineData(@"http://0xd83ad6ce")]
    [InlineData(@"http://www.whitelisteddomain.tld@0xd83ad6ce")]
    [InlineData(@"http://3H6k7lIAiqjfNeN@0xd83ad6ce")]
    [InlineData(@"http://XY>.7d8T\205pZM@0xd83ad6ce")]
    [InlineData(@"http://3627734734")]
    [InlineData(@"http://www.whitelisteddomain.tld@3627734734")]
    [InlineData(@"http://3H6k7lIAiqjfNeN@3627734734")]
    [InlineData(@"http://XY>.7d8T\205pZM@3627734734")]
    [InlineData(@"http://472.314.470.462")]
    [InlineData(@"http://www.whitelisteddomain.tld@472.314.470.462")]
    [InlineData(@"http://3H6k7lIAiqjfNeN@472.314.470.462")]
    [InlineData(@"http://XY>.7d8T\205pZM@472.314.470.462")]
    [InlineData(@"http://0330.072.0326.0316")]
    [InlineData(@"http://www.whitelisteddomain.tld@0330.072.0326.0316")]
    [InlineData(@"http://3H6k7lIAiqjfNeN@0330.072.0326.0316")]
    [InlineData(@"http://XY>.7d8T\205pZM@0330.072.0326.0316")]
    [InlineData(@"http://00330.00072.0000326.00000316")]
    [InlineData(@"http://www.whitelisteddomain.tld@00330.00072.0000326.00000316")]
    [InlineData(@"http://3H6k7lIAiqjfNeN@00330.00072.0000326.00000316")]
    [InlineData(@"http://XY>.7d8T\205pZM@00330.00072.0000326.00000316")]
    [InlineData(@"http://[::216.58.214.206]")]
    [InlineData(@"http://www.whitelisteddomain.tld@[::216.58.214.206]")]
    [InlineData(@"http://3H6k7lIAiqjfNeN@[::216.58.214.206]")]
    [InlineData(@"http://XY>.7d8T\205pZM@[::216.58.214.206]")]
    [InlineData(@"http://[::ffff:216.58.214.206]")]
    [InlineData(@"http://www.whitelisteddomain.tld@[::ffff:216.58.214.206]")]
    [InlineData(@"http://3H6k7lIAiqjfNeN@[::ffff:216.58.214.206]")]
    [InlineData(@"http://XY>.7d8T\205pZM@[::ffff:216.58.214.206]")]
    [InlineData(@"http://0xd8.072.54990")]
    [InlineData(@"http://www.whitelisteddomain.tld@0xd8.072.54990")]
    [InlineData(@"http://3H6k7lIAiqjfNeN@0xd8.072.54990")]
    [InlineData(@"http://XY>.7d8T\205pZM@0xd8.072.54990")]
    [InlineData(@"http://0xd8.3856078")]
    [InlineData(@"http://www.whitelisteddomain.tld@0xd8.3856078")]
    [InlineData(@"http://3H6k7lIAiqjfNeN@0xd8.3856078")]
    [InlineData(@"http://XY>.7d8T\205pZM@0xd8.3856078")]
    [InlineData(@"http://00330.3856078")]
    [InlineData(@"http://www.whitelisteddomain.tld@00330.3856078")]
    [InlineData(@"http://3H6k7lIAiqjfNeN@00330.3856078")]
    [InlineData(@"http://XY>.7d8T\205pZM@00330.3856078")]
    [InlineData(@"http://00330.0x3a.54990")]
    [InlineData(@"http://www.whitelisteddomain.tld@00330.0x3a.54990")]
    [InlineData(@"http://3H6k7lIAiqjfNeN@00330.0x3a.54990")]
    [InlineData(@"http://XY>.7d8T\205pZM@00330.0x3a.54990")]
    [InlineData(@"http:0xd8.0x3a.0xd6.0xce")]
    [InlineData(@"http:www.whitelisteddomain.tld@0xd8.0x3a.0xd6.0xce")]
    [InlineData(@"http:3H6k7lIAiqjfNeN@0xd8.0x3a.0xd6.0xce")]
    [InlineData(@"http:XY>.7d8T\205pZM@0xd8.0x3a.0xd6.0xce")]
    [InlineData(@"http:0xd83ad6ce")]
    [InlineData(@"http:www.whitelisteddomain.tld@0xd83ad6ce")]
    [InlineData(@"http:3H6k7lIAiqjfNeN@0xd83ad6ce")]
    [InlineData(@"http:XY>.7d8T\205pZM@0xd83ad6ce")]
    [InlineData(@"http:3627734734")]
    [InlineData(@"http:www.whitelisteddomain.tld@3627734734")]
    [InlineData(@"http:3H6k7lIAiqjfNeN@3627734734")]
    [InlineData(@"http:XY>.7d8T\205pZM@3627734734")]
    [InlineData(@"http:472.314.470.462")]
    [InlineData(@"http:www.whitelisteddomain.tld@472.314.470.462")]
    [InlineData(@"http:3H6k7lIAiqjfNeN@472.314.470.462")]
    [InlineData(@"http:XY>.7d8T\205pZM@472.314.470.462")]
    [InlineData(@"http:0330.072.0326.0316")]
    [InlineData(@"http:www.whitelisteddomain.tld@0330.072.0326.0316")]
    [InlineData(@"http:3H6k7lIAiqjfNeN@0330.072.0326.0316")]
    [InlineData(@"http:XY>.7d8T\205pZM@0330.072.0326.0316")]
    [InlineData(@"http:00330.00072.0000326.00000316")]
    [InlineData(@"http:www.whitelisteddomain.tld@00330.00072.0000326.00000316")]
    [InlineData(@"http:3H6k7lIAiqjfNeN@00330.00072.0000326.00000316")]
    [InlineData(@"http:XY>.7d8T\205pZM@00330.00072.0000326.00000316")]
    [InlineData(@"http:[::216.58.214.206]")]
    [InlineData(@"http:www.whitelisteddomain.tld@[::216.58.214.206]")]
    [InlineData(@"http:3H6k7lIAiqjfNeN@[::216.58.214.206]")]
    [InlineData(@"http:XY>.7d8T\205pZM@[::216.58.214.206]")]
    [InlineData(@"http:[::ffff:216.58.214.206]")]
    [InlineData(@"http:www.whitelisteddomain.tld@[::ffff:216.58.214.206]")]
    [InlineData(@"http:3H6k7lIAiqjfNeN@[::ffff:216.58.214.206]")]
    [InlineData(@"http:XY>.7d8T\205pZM@[::ffff:216.58.214.206]")]
    [InlineData(@"http:0xd8.072.54990")]
    [InlineData(@"http:www.whitelisteddomain.tld@0xd8.072.54990")]
    [InlineData(@"http:3H6k7lIAiqjfNeN@0xd8.072.54990")]
    [InlineData(@"http:XY>.7d8T\205pZM@0xd8.072.54990")]
    [InlineData(@"http:0xd8.3856078")]
    [InlineData(@"http:www.whitelisteddomain.tld@0xd8.3856078")]
    [InlineData(@"http:3H6k7lIAiqjfNeN@0xd8.3856078")]
    [InlineData(@"http:XY>.7d8T\205pZM@0xd8.3856078")]
    [InlineData(@"http:00330.3856078")]
    [InlineData(@"http:www.whitelisteddomain.tld@00330.3856078")]
    [InlineData(@"http:3H6k7lIAiqjfNeN@00330.3856078")]
    [InlineData(@"http:XY>.7d8T\205pZM@00330.3856078")]
    [InlineData(@"http:00330.0x3a.54990")]
    [InlineData(@"http:www.whitelisteddomain.tld@00330.0x3a.54990")]
    [InlineData(@"http:3H6k7lIAiqjfNeN@00330.0x3a.54990")]
    [InlineData(@"http:XY>.7d8T\205pZM@00330.0x3a.54990")]
    [InlineData(@"〱google.com")]
    [InlineData(@"〵google.com")]
    [InlineData(@"ゝgoogle.com")]
    [InlineData(@"ーgoogle.com")]
    [InlineData(@"ｰgoogle.com")]
    [InlineData(@"/〱google.com")]
    [InlineData(@"/〵google.com")]
    [InlineData(@"/ゝgoogle.com")]
    [InlineData(@"/ーgoogle.com")]
    [InlineData(@"/ｰgoogle.com")]
    [InlineData(@"%68%74%74%70%3a%2f%2f%67%6f%6f%67%6c%65%2e%63%6f%6d")]
    [InlineData(@"http://%67%6f%6f%67%6c%65%2e%63%6f%6d")]
    [InlineData(@"<>javascript:alert(1);")]
    [InlineData(@"<>//google.com")]
    [InlineData(@"//google.com\@www.whitelisteddomain.tld")]
    [InlineData(@"https://:@google.com\@www.whitelisteddomain.tld")]
    [InlineData(@"\x6A\x61\x76\x61\x73\x63\x72\x69\x70\x74\x3aalert(1)")]
    [InlineData(@"\u006A\u0061\u0076\u0061\u0073\u0063\u0072\u0069\u0070\u0074\u003aalert(1)")]
    [InlineData(@"ja\nva\tscript\r:alert(1)")]
    [InlineData(@"\j\av\a\s\cr\i\pt\:\a\l\ert\(1\)")]
    [InlineData(@"\152\141\166\141\163\143\162\151\160\164\072alert(1)")]
    [InlineData(@"http://google.com:80#@www.whitelisteddomain.tld/")]
    [InlineData(@"http://google.com:80?@www.whitelisteddomain.tld/")]
    [InlineData(@"http://google.com\www.whitelisteddomain.tld")]
    [InlineData(@"http://google.com&www.whitelisteddomain.tld")]
    [InlineData(@"http:///////////google.com")]
    [InlineData(@"\\google.com")]
    [InlineData(@"http://www.whitelisteddomain.tld.google.com")]
    [Theory]
    public async Task WhenMaliciousRequest_ShouldThrowException(string baaaddddd)
    {
        var testSession = new TestSession();

        var actual = async () => await testSession.SetUserPreferredLandingPageAsync(baaaddddd);

        await actual.Should().ThrowAsync<NotSupportedException>();
    }
}