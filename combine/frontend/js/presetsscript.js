console.log("hello1");

const subscriptionpresets = [
    {
        "id" : "netflix1",
        "description" : "Netflix Basic",
        "url" : "netflix.com",
        "price" : 8.99,
        "icon" : "images/netflixicon.png"
    },
    {
        "id" : "netflix2",
        "description" : "Netflix Standard",
        "url" : "netflix.com",
        "price" : 13.99,
        "icon" : "images/netflixicon.png"
    },
    {
        "id" : "netflix3",
        "description" : "Netflix Premium",
        "url" : "netflix.com",
        "price" : 17.99,
        "icon" : "images/netflixicon.png"
    },
    {
        "id" : "hulu",
        "description" : "Hulu",
        "url" : "hulu.com",
        "price" : 5.99,
        "icon" : "images/huluicon.png"
    },
    {
        "id" : "hbomax",
        "description" : "HBO Max",
        "url" : "hbomax.com",
        "price" : 14.99,
        "icon" : "images/hboicon.png"
    },
    {
        "id" : "amazonprime",
        "description" : "Amazon Prime Video",
        "url" : "amazon.com",
        "price" : 8.99,
        "icon" : "images/amazonprimeicon.png"
    },
    {
        "id" : "disneyplus",
        "description" : "Disney Plus",
        "url" : "disneyplus",
        "price" : 6.99,
        "icon" : "images/disneyplusicon.png"
    },
    {
        "id" : "starz",
        "description" : "Starz",
        "url" : "starz.com",
        "price" : 8.99,
        "icon" : "images/starzicon.png"
    },
    {
        "id" : "mubi",
        "description" : "Mubi",
        "url" : "mubi.com",
        "price" : 10.99,
        "icon" : "images/mubiicon.png"
    }
]

//fill preseticons
console.log("hello");
function fillpreseticons(presetid){
    console.log(presetid);
    for(const service of subscriptionpresets){
        console.log(service);
        document.getElementById(presetid).innerHTML += `<a href="#" id=${service.id}><div class="iconbox"><img src="${service.icon}" alt=""></div>${service.description}, $${service.price}</a>`
    }
    document.getElementById(presetid).innerHTML += `<a href="add.html"><div class="iconbox"><img src="icon.png" alt=""></div>Other</a>`
}

fillpreseticons("preseticons");