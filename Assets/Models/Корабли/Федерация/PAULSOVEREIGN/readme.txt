Starship U.S.S. Enterprise NCC-1701-E Re-textured.
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
class:		Sovereign
release:	v1.0 (30.04.2003)
faces:		whoa... 1,183,225
software:	Rhinoceros, 3D Studio MAX 2, Paint Shop Pro 6, 3ds max 5.1, Adobe Photoshop
build time:	12/2001 - 04/2003
mesh by:	Paul Trenkler (Isidor) Re-textured by Daniel Broadway (ChiefLDS or PixelMagic)
email:		isidor@palko.sk  Daniel Broadway (ChiefLDS) email: chief_lds@hotmail.com
m. phone:	+421 904 936389 (just send an SMS in case of emergency :))

Made with 100% recycled electrons in Slovakia :o) 
No animals were harmed during the contruction of this mesh :)
**********************************************************
Hi!

Well, it looks like you got yourself a copy of my Sovereign mesh. I started this project at Christmas 2001 with the goal to make a mesh as realistic as the studio model, no matter what the polycount will be :o). I thought that it would take six months, but obviously it was three times that much. Some of you had already seen WIPs of the mesh, and thought I have abbandoned it. Nope. I just didn't want you to be disappointed if I'd stop working on it again, so I didn't tell you :). However, now it's finally finished and I'm happy to release it. Here's some basic info:

I. THE OBJECTS
~~~~~~~~~~~~~~
The objects are divided into 5 groups: SAUCER TOP, SAUCER MIDDLE, SAUCER BOTTOM, SECONDARY HULL and the NACELLES. So if you do a bottom shot you don't need the saucer top in the scene so you can delete/hide it. The "saucer middle" group contains the thin saucer side hull and the impulse engines, which are visible from both above and below.

Anyway, these five groups consist of a huge amount of sub-groups, for example "shuttle bay". They contain (mostly) the actual meshes, with names like "Obj1" (really - I didn't have the imagination to write hundreds of unique names ;-))
Almost all details are fully modeled, including extruded windows, individually numbered escape pods, registry numbers, many hull gridlines and miscellaneous tiny hull details.
Not modelled: interiors (with exceptions...:o), tiny hull plates, phaser array stripes.

I hope there are no visible mesh errors included, let me know if you find any.

II. THE MATERIALS
~~~~~~~~~~~~~~~~~
HULL - there are 2 basic types of the hull - lighter and darker grey. These are applied to the objects either directly or through a mask (blend material), like "hull.tga".
Because of different objects, there are several blend materials applied individually to objects (e.g. "_hull saucer top").
The 2 hull types have different specular and shininess properties, hopefully resulting in quite realistic light effects...
There are also other (darker) types of hull applied to objects over the whole ship.

You'll see that each cathegory of objects (escape pods, hull markings, phaser stripes) has it's own materials, making it quite easy to change the appearance of the ship by modifying just a few materials. If you experience problems with the materials, let me know and I shall make appropriate changes to the next release.

I set up a few simple video post effects, like glows on nav lights, impulse and warp engines, bussard collectors etc. If you'd like to play with these effects, you'll need the material IDs of those objects:
1 - navigational lights
2 - warp drive (blue)
3 - bussards, impulse engines
4 - lit windows
5 - the deflector

III. EXTRAS ;-)
~~~~~~~~~~~~~~~
Now to the fun part...

SHUTTLE BAY 1
It's located at the back of saucer top.
The door is in group "shuttle bay", named "shuttle bay DOOR" and by deleting (moving) this two objects you can "open" the shuttle bay. Interior is simple, but sufficient for distance shots.

SHUTTLE BAY 2
Located at secondary hull back - group "secondary hull" -> "shuttle bay 2".
It has two doors - a closed one (object "shuttle_bay 2 DOOR") and an opened one (under the original door). It would be difficult to animate door opening here, so you should only delete the closed door.

CAPTAIN'S YACHT
Yep, I modelled also a primitive version of the yacht as seen in "ST:Insurrection". I had to solve the problem of positioning the nacelles, so I booleaned two "boxes" into the hull. The holes are covered, so to "loosen" the yacht, first delete the group "yacht (delete to open)", then move/animate the group "captain's YACHT". You should also animate the nacelles turning (subgroups "yacht nacelle left" and "yacht nacelle right") into the correct positions - just rotate them along the cylinder-shaped objects on the yacht.
There are also some basic hull structures below the yacht, so it's possible to render the ship without the yacht, though it won't look very good on close-ups.

IV. CONDITIONS OF USE
~~~~~~~~~~~~~~~~~~~~~
1, YOU MUST:

- put credits on all images made with the mesh. Just place a text like "Sovereign by Paul Trenkler" in the corner, please DON'T use my nickname (Isidor) unless you have a VERY good reason to do so :). If you like, you can also write my email or homepage adress, but you don't have to.

2, YOU MUST NOT: (i.e. you CAN'T)

- use the mesh for profit-making projects

- sell the mesh directly or by putting it on a CD/DVD/other media which will be sold
(exceptions may be done if you contact me)

- put the mesh on public download (i.e. making it available to an unknown amount of people)
without my permission. Expect me to allow this only for reliable and good-known sites.

- release any conversions without my permission, just ask me and I'll say "OK". However, doing any conversion should be quite difficult (polycount, materials...).

- modify any of the files before redistributing the mesh, exceptions may be allowed by me.

3, I'D BE HAPPY IF YOU:

- notify me of any image/anim you do with the ship. Really, I'm not good at making pics so I'd love to see her "in action" :o). Just send me an email with the file URL or attached. Thanks :)

- send me comments, critics, anything related to this mesh. It will be considered when making a (possible) next release of the ship.

If you don't agree to these rules, you should delete the mesh immediately.
Any violation of this rules will be investigated thoroughly and it may happen that the mesh will be removed from download. In this case, all of you who own a copy will have to delete it without further notice. I'd be very sorry to do that, so please don't force me to do so.

That's about it. Don't hesitate to ask if you have questions, you can reach me through email or mobile phone :). I sincerely hope you'll have fun working with the mesh and don't kill me for the polycount :o).

Isidor

PS: Do you think she's good enough for the "Insane detail club"? LOL :o)
**********************************************************
One more thing though. I didn't mention it, but this mesh was made on a P-II 366, 64 MB RAM and TNT2 card. So, you see that's pretty poor. Anybody willing to donate a few bucks for a PC upgrade is welcome. Really, each $/euro/whatever will be appreciated and invested wisely. If you don't like this idea, just send me a postcard of your home town or something nice. All of you who reply in any way will be mentioned on my homepage. Thanks in advance.

my adress:
Paul Trenkler
Csl. armady 6
040 01 Kosice
Slovakia (Europe)
