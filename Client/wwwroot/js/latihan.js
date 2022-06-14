//console.log("tes latihan")

var a = 1;
//console.log(a);
a = "tessss";
console.log(a);

//array 1 dimensi
let array = [1, 2, 3, 4, "tes"];
//console.log(array);

//insert array di belakang
array.push("hallo");
//console.log(array);

//hapus array di belakang
array.pop();
//console.log(array);

//tambah depan
array.unshift("depan");
//console.log(array);

//hapus depan
array.shift();
//console.log(array);

//array multidimensi
let arrayMulti = ['a', 'b', 'c', [1, 2, 3, ["hallo!"]], true];
//console.log(arrayMulti[3][3][0]);

//deklarasi objek
//json = javascripb object notation
let mhs = {
    nama: "budi",
    nim: "a112015",
    urusan: "TI",
    umur: 80,
    hobby: ["mancing", "ngegame", "ngewibu"],
    isActive: true
}
//console.log(mhs.hobby[2]);

let user = {}; //object
user.username = "budi";
user.password = "123abc";
//console.log(user);

//misahin data
const csv = "1|2|3";
const [one, two, three] = csv.split("|");
console.log(three);

//bug javaScript
console.log(halo);
var halo = "tes"

//array of object -> kayak api kemarin
const animals = [
    { name: "Garfield", species: "cat", class: { name: "mamalia" } },
    { name: "Nemo", species: "fish", class: { name: "invertebrata" } },
    { name: "Tom", species: "cat", class: { name: "mamalia" } },
    { name: "Bruno", species: "fish", class: { name: "invertebrata" } },
    { name: "Carlo", species: "cat", class: { name: "mamalia" } },
]
//console.log(animals);

//TUGAS

//1. jika spesies = cat, maka ambil objeknya dan masukkan ke 1 variabel
let onlyCat = [];

for (var i = 0; i < animals.length; i++) {
    if (animals[i].species == "cat")
        onlyCat.push(animals[i]);
}
console.log(onlyCat);

//2. jika spesies = fish, cari namenya. Jika name = invertebrata  maka diganti jd name = non - mamalia

for (var i = 0; i < animals.length; i++) {
    if (animals[i].species == "fish")
        animals[i].class.name = "non - mamalia";
}

console.log(animals);

///

//const animals = [
//    { name: "Garfield", species: "cat", class: { name: "mamalia" } },
//    { name: "Nemo", species: "fish", class: { name: "invertebrata" } },
//    { name: "Tom", species: "cat", class: { name: "mamalia" } },
//    { name: "Bruno", species: "fish", class: { name: "invertebrata" } },
//    { name: "Carlo", species: "cat", class: { name: "mamalia" } },
//]
//console.log(animals);

//jQuery
//let h1 = $("h1");
//console.log(h1);

//$("h1").html("testing diubah dari juery"); //innerHTML
//console.log(h1);

$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/",
}).done(result => { //result -> nama var bebas
    //console.log(result.results[0].name);
    console.log(result.results);
    let text = "";
    $.each(result.results, function (key, val) {
        //text += '<li>${val.name}</li>' //list
        text += `<tr>
                    <td>${key+1}</td>
                    <td>${val.name}</td>
                    <td>
                        <button type="button" onclick="detailPoke('${val.url}')" class="btn btn-primary" data-toggle="modal" data-target="#modalDetail">Detail</button>
                    </td>
                 </tr>`
    });
    //console.log(text);
    $("#tablePoke").html(text);
}).fail((error) => {
    console.log(error)
});


$('button').on('click', function () {
    alert("Ini halaman home");
})


function detailPoke(urlPoke) {
    $.ajax({
        url: urlPoke, //ambil url pokemon
    }).done(result => {
        //nama pokemon
        let name = " ";
        name = `${result.name}`; 
        $("#modalName").html(name);

        //gambar pokemon
        $("#modalImage").attr("src", result.sprites.other.dream_world.front_default);
        //let img = `<img src="${result.sprites.other.dream_world.front_default}" class="img-thumbnail">`
        //$("#modalGambar").html(img);

        //pill badges -> types
        let types = [];

        $.each(result.types, function (key, val) {
            types += `<span class="badge badge-danger">${val.type.name}</span>`
            //types += `${result.types.type.name}`
        });
        $("#modalTypes").html(types);

        //<div id="modalNamaStats"></div>
        //<div class="progress-bar progress-bar-striped bg-warning" role="progressbar" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"></div>

        //stats -> skor
        let namaStats = " ";
        let skor = " ";
        //$.each(result.stats, function (key, val) {
        //    namaStats += `<p>${result.stats.stat[val].name}</p>`;
        //    skor += `<div class="progress">
        //                    <div class="progress-bar progress-bar-striped bg-warning" role="progressbar" style="width: ${val.base_stat}%;" aria-valuenow="${val.base_stat}" aria-valuemin="0" aria-valuemax="100"></div>
        //             </div >`
        //});
        //console.log(namaStats);
        //$("#modalNamaStats").html(namaStats);
        //$("#modalStats").html(skor);

        for (var i = 0; i < result.stats.length; i++) {
            //namaStats += `<p>${result.stats[i].stat.name}</p>`;
            skor += `<p>${result.stats[i].stat.name}</p><div class="progress"><div class="progress-bar progress-bar-striped bg-warning" role="progressbar" style="width: ${result.stats[i].base_stat}%;" aria-valuenow="${result.stats[i].base_stat}" aria-valuemin="0" aria-valuemax="100"></div></div >`
        }
        console.log(namaStats);
        console.log(skor);

        $("#modalStat").empty();
        $("#modalStat").append(skor);

    }).fail((error) => {
        console.log(error)
    });
}


