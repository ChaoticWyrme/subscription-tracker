const subscriptionpresets = [
  {
    id: "netflix",
    description: "Netflix Basic",
    url: "www.netflix.com",
    price: 8.99,
    icon: "images/netflixicon.png",
  },
  {
    id: "hulu",
    description: "Hulu",
    url: "www.hulu.com",
    price: 5.99,
    icon: "images/huluicon.png",
  },
  {
    id: "hbomax",
    description: "HBO Max",
    url: "www.hbomax.com",
    price: 14.99,
    icon: "images/hboicon.png",
  },
  {
    id: "amazonprime",
    description: "Amazon Prime Video",
    url: "www.amazon.com",
    price: 8.99,
    icon: "images/amazonprimeicon.png",
  },
  {
    id: "disneyplus",
    description: "Disney Plus",
    url: "www.disneyplus.com",
    price: 6.99,
    icon: "images/disneyplusicon.png",
  },
  {
    id: "starz",
    description: "Starz",
    url: "www.starz.com",
    price: 8.99,
    icon: "images/starzicon.png",
  },
  {
    id: "mubi",
    description: "Mubi",
    url: "www.mubi.com",
    price: 10.99,
    icon: "images/mubiicon.png",
  },
];

//fill preseticons
function fillpreseticons(presetid) {
  console.log("fill");
  if(document.getElementById(
    presetid
  ) === null) return;
  for (const service of subscriptionpresets) {
    document.getElementById(
      presetid
    ).innerHTML += `<a onclick='addpreset(${service.id})' id=${service.id}><div class="iconbox"><img src="${service.icon}" alt=""></div>${service.description}, $${service.price}</a>`;
  }
  document.getElementById(
    presetid
  ).innerHTML += `<a href="add.html"><div class="iconbox"><img src="icon.png" alt=""></div>Other</a>`;
}

fillpreseticons("preseticons");

async function addpreset(subid){
  console.log(subid);
  
  let storage = await browser.storage.local.get(["subList"]);

  let subList = storage["subList"] || [];

  var index = -1;
  for(var k = 0; k < subscriptionpresets; k++){
    if(subscriptionpresets[k].id==subid){
      index = k;
      break;
    }
  }

  let name = subscriptionpresets[index].id;
  let price = subscriptionpresets[index].price;
  let url = subscriptionpresets[index].url;
  let icon = subscriptionpresets[index].icon;

  subList.push({
    name,
    price,
    url,
    icon
  });

  console.log(subList);

  await browser.storage.local.set({ subList });

  window.location.href = "home.html";


}
