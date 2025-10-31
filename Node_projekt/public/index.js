var parbeszed = document.getElementById("parbeszed");
let szovegek = [
    `<div class="alert alert-success" data-aos="fade-up" data-aos-duration="1500" role="alert">
    <strong>Riporter:</strong> Sziasztok, ez itt a mai podcast, és a téma nem más, mint a Hideg idők és a tél – meg az,
     hogy hogyan ne dőljünk ki már az első hideg napon. Itt van velem... hát, én magam. 
  </div>`,

    `<div class="alert alert-success" data-aos="fade-up" data-aos-duration="1500" role="alert">
    <strong>Riporter:</strong> Kezdjük az alapoknál: miért lesz mindenki beteg, amint leesik a hőmérséklet tíz fok alá?
  </div>`,

    `<div class="alert alert-info" data-aos="fade-up" data-aos-duration="1500" role="alert">
    <strong>Vendég:</strong> Mert sokan nem tudják hogyan kell normálisan felöltözni. Van, 
    aki mínuszban is pólóban mászkál, mert „nem fázik”. Aztán három nap múlva már orrspray nélkül levegőt se kap. 
    A hideg önmagában nem betegít meg, de legyengíti az immunrendszert, és a vírusok ezt kihasználják.
  </div>`,

    `<div class="alert alert-success" data-aos="fade-up" data-aos-duration="1500" role="alert">
    <strong>Riporter:</strong> Szóval nem elég, ha csak melegen öltözöm, ugye?
  </div>`,

    `<div class="alert alert-info" data-aos="fade-up" data-aos-duration="1500" role="alert">
    <strong>Vendég:</strong> Igen, kell mellette alvás, normális étkezés, meg néha vitamin is.
     Nem kell túlzásba vinni, de ha egész nap csak energiaitalt és péksütit eszel, ne csodálkozz, ha az első köhögés ledönt. 
  </div>`,

    `<div class="alert alert-success" data-aos="fade-up" data-aos-duration="1500" role="alert">
    <strong>Riporter:</strong> És a suli? Ott meg elég egy ember, aki köhög, és mindenki elkapja.
  </div>`,

    `<div class="alert alert-info" data-aos="fade-up" data-aos-duration="1500" role="alert">
    <strong>Vendég:</strong> Pontosan. Ezért kéne otthon maradni, ha már valaki beteg, de persze senki sem akar hiányozni. 
    Így aztán végül az egész osztály beteg lesz. 
  </div>`,

    `<div class="alert alert-success" data-aos="fade-up" data-aos-duration="1500" role="alert">
    <strong>Riporter:</strong> Van valami, amit tényleg érdemes csinálni, hogy elkerüljük?
  </div>`,
    `<div class="alert alert-info" data-aos="fade-up" data-aos-duration="1500" role="alert">
  <strong>Vendég:</strong> Őszintén? Igen. Nem kell nagy dolgokra gondolni — csak kicsit többet kéne mozogni,
    egy séta is számít. A friss levegő tényleg sokat segít, még ha hideg is van. Meg az sem árt ha néha rendes ételt eszünk.
     És igen, vizet is lehet inni, nem csak kávét meg kólát.
</div>`,

    `<div class="alert alert-success" data-aos="fade-up" data-aos-duration="1500" role="alert">
    <strong>Riporter:</strong> És ha már megtörtént a baj, mit érdemes tenni?
  </div>`,

    `<div class="alert alert-info" data-aos="fade-up" data-aos-duration="1500" role="alert">
    <strong>Vendég:</strong> Pihenni. Nem kell iskolába menni lázzal. Egy nap otthon többet ér,
     mint egy hét szenvedés. És ha lehet, sok meleg folyadékot igyál, és próbáld kipihenni.
  </div>`,

    `<div class="alert alert-success" data-aos="fade-up" data-aos-duration="1500" role="alert">
    <strong>Riporter:</strong> Jó, akkor zárásként: mi az az egy dolog, amit mindenki megfogadhatna télen?
  </div>`,

    `<div class="alert alert-info" data-aos="fade-up" data-aos-duration="1500" role="alert">
    <strong>Vendég:</strong> Hallgass a testedre. Ha fáradt vagy, aludj. Ha fázol, öltözz fel.
     Ha beteg vagy, pihenj. Ennyi. Meg szedjél vitamint.
  </div>`,

    `<div class="alert alert-warning" data-aos="fade-up" data-aos-duration="1500" role="alert">
    <em>Köszönöm, hogy meghallgattatok!</em> Ez volt a „Túlélni a telet” című podcast, ahol önmagammal vitatkoztam arról, miért nem kéne decemberben pólóban menni iskolába. Vigyázzatok magatokra, és ne feledjétek: a meleg tea mindig jobb, mint a hideg orrcsepp.
  </div>`
];
szovegek.forEach((sz, i) => {
    setTimeout(() => {

        parbeszed.innerHTML += sz;
    }, i * 1500); // 2 másodpercenként
});

