package com.example.pw2.pb3;

import com.example.pw2.Service;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

import java.io.IOException;
import java.io.PrintWriter;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

@WebServlet(name = "pb3Comments", value = "/pb3/public_page")
public class PublicPageServlet extends HttpServlet {
    @Override
    public void doGet(HttpServletRequest request, HttpServletResponse response) throws IOException {
        response.setContentType("text/html");

        String text="Cicăerau odată o babă și un moșneag: moșneagul de-o sută de ani, și baba de nouăzeci; și amândoi bătrânii aceștia erau albi ca iarna și posomorâți ca vremea cea rea din pricină că nu aveau copii. Și, Doamne! tare mai erau doriți să aibă măcar unul, căci, cât era ziulica și noaptea de mare, ședeau singurei ca cucul și le țiuiau urechile, de urât ce le era. Și apoi, pe lângă toate aceste, nici vreo scofală mare nu era de dânșii: un bordei ca vai de el, niște țoale rupte, așternute pe laițe, și atâta era tot. Ba de la o vreme încoace, urâtul îi mânca și mai tare, căci țipenie de om nu le deschidea ușa; parcă erau bolnavi de ciumă, sărmanii! În una din zile, baba oftă din greu și zise moșneagului: — Doamne, moșnege, Doamne! De când suntem noi, încă nu ne-a zis nime tată și mamă! Oare nu-i păcat de Dumnezeu că mai trăim noi pe lumea asta? Căci la casa fără de copii nu cred că mai este vrun Doamne-ajută! — Apoi dă, măi babă, ce putem noi face înaintea lui Dumnezeu? — Așa este, moșnege, văd bine; dar, până la una, la alta, știi ce-am gândit eu astă-noapte? — Știu, măi babă, dacă mi-i spune. — Ia, mâine dimineață, cum s-a miji de ziuă, să te scoli și să apuci încotro-i vedea cu ochii; și ce ți-a ieși înainte întâi și-ntâi, dar a fi om, da' șarpe, da', în sfârșit, orice altă jivină a fi, pune-o în traistă și o adă acasă; vom crește-o și noi cum vom putea, și acela să fie copilul nostru. Moșneagul, sătul și el de-atâta singurătate și dorit să aibă copii, se scoală a doua zi dis-dimineață, își ia traista în băț și face cum i-a zis baba... Pornește el și se duce tot înainte pe niște ponoare, până ce dă peste un bulhac. Și numai iaca că vede în bulhac o scroafă cu doisprezece purcei, care ședeau tologiți în glod și se păleau la soare. Scroafa, cum vede pe moșneag că vine asupra ei, îndată începe a grohăi, o rupe de fugă, și purceii după dânsa. Numai unul, care era mai ogârjit, mai răpănos și mai răpciugos, neputând ieși din glod, rămase pe loc. Moșneagul degrabă îl prinde, îl bagă în traistă, așa plin de glod și de alte podoabe cum era, și pornește cu dânsul spre casă. — Slavă ție, Doamne! zise moșneagul, că pot să duc babei mele o mângâiere! Mai știu eu? Poate ori Dumnezeu, ori dracul i-a dat în gând ieri noapte de una ca asta. Și cum ajunge-acasă, zice: — Iaca, măi băbușcă, ce odor ți-am adus eu! Numai să-ți trăiască! Un băiat ochios, sprâncenat și frumușel de nu se mai poate. Îți seamănă ție, ruptă bucățică!... Acum pune de lăutoare și grijește-l cum știi tu că se grijesc băieții: că, după cum vezi, îi cam colbăit, mititelul! — Moșnege, moșnege! zise baba, nu râde, că și aceasta-i făptura lui Dumnezeu; ca și noi... Ba poate... și mai nevinovat, sărmanul! Apoi, sprintenă ca o copilă, face degrabă leșie, pregătește de scăldătoare și, fiindcă știa bine treaba moșitului, lă purcelul, îl scaldă, îi trage frumușel cu untură din opaiț pe la toate încheieturile, îl strânge de nas și-l sumuță, ca să nu se deoache odorul. Apoi îl piaptănă și-l grijește așa de bine, că peste câteva zile îl scoate din boală; și cu tărâțe, cu cojițe, purcelul începe a se înfiripa și a crește văzând cu ochii, de-ți era mai mare dragul să te uiți la el. Iară baba nu știa ce să mai facă de bucurie că are un băiat așa de chipos, de hazliu, de gras și învelit ca un pepene. Să-i fi zis toată lumea că-i urât și obraznic, ea ținea una și bună, că băiat ca băiatul ei nu mai este altul! Numai de-un lucru era baba cu inima jignită: că nu putea să le zică tată și mamă.";
        for(var expr : Service.getForbiddenExpressions()){
            var p = Pattern.compile(expr);
            Matcher m = p.matcher(text);
            text = m.replaceAll("***");
        }

        PrintWriter out = response.getWriter();
        out.println(text);
    }


    public void destroy() {
    }
}
