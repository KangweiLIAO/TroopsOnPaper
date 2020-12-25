
--------------------------------------------------------------------------------
	Isle of Lore 2: Hex Tiles Regular
--------------------------------------------------------------------------------

- Introduction & Contact
- License
- Contents
- Content Structure
- Specification
- How to Use
- How to Use: Hex Kit
- How to Use: Tiled Editor
- How to Use: Source Files
- Krita
- Modifications

--------------------------------------------------------------------------------
	Introduction & Contact
--------------------------------------------------------------------------------

Hey there!

Thank you so much for buying the next edition of my Isle of Lore Hex Tiles,
this time with a new greenish color palette, a more delicate way of illustrating
and in a regular hex shape which makes the pack compatible with your favorite
hex mapping software! There will be a new pack in an isometric style similar
to the original Isle of Lore Hex Tiles in the near future, so make sure to
subscribe to my mailing list!

	Isle of Lore: Hex Tiles					(the original one)
	Isle of Lore 2: Hex Tiles Regular		(the one you just bought)
	Isle of Lore 2: Hex Tiles				(upcoming)

Also, if you encounter any problem, got a question or have some feedback,
you can reach me easily via mail (see below).

By the way, I may update this asset pack (at no additional cost) in the future
and if you want to keep up-to-date and learn more about my other game-related
projects, you can subscribe to my mailing list or join my Discord.

	Mailing List: 	http://mailinglist.stevencolling.com/game_assets/
	Discord:		http://discord.stevencolling.com
	Mail:			info@stevencolling.com
	Website:		http://www.stevencolling.com

See you there!
Steven Colling

--------------------------------------------------------------------------------
	License
--------------------------------------------------------------------------------

Depending on where you bought this asset, the store may provide
license terms for the assets they sell, including this one. If that's not the
case, you'll find a "License.txt" file in the topmost directory. Please refer
to either the storefront's asset license or to the license file provided with
the assets. If you think the seller's license is too restricting for your usage,
please reach out via mail: info@stevencolling.com.

--------------------------------------------------------------------------------
	Contents
--------------------------------------------------------------------------------

Isle of Lore 2: Hex Tiles Regular contains...

- 726 Image Files (png)
	- 321 Tiles
		- 20 City Tiles
		- 40 Conifer Forest Tiles
		- 40 Deciduous Forest Tiles
		- 40 Grassland Tiles
		- 40 Hill Tiles
		- 40 Moor Tiles
		- 40 Valley Tiles
		- 40 Mountain Peak Tiles
		- 21 Ocean Tiles
	- 120 Locations Icons (30 Location Icons in 4 variants)
	- 285 Overlays
		- 55 Path Overlays
		- 55 Road Overlays
		- 55 River Overlays
		- 60 Flair Overlays
		- 60 Marker Overlays

- 42 Sprite Sheets (png; image files compiled)
	- 33 Tile Sprite Sheets
	- 9 Overlay Sprite Sheets

- 327 Source Files (Krita .kra and Photoshop .psd)
	- 321 Tile Source Files
		- 20 City Source Files
		- 40 Conifer Forest Source Files
		- 40 Deciduous Forest Source Files
		- 40 Grassland Source Files
		- 40 Hill Source Files
		- 40 Moor Source Files
		- 40 Valley Source Files
		- 40 Mountain Peak Source Files
		- 21 Ocean Source Files
	- 1 Location Source File
	- 5 Overlay Source Files

--------------------------------------------------------------------------------
	Content Structure
--------------------------------------------------------------------------------

In the topmost directory:

./Images			all sources exported to png files
						every overlay and tile type has its own sub-folder
./Images Legible	the same as "./Images", but with more legible instead of
						unique names (use the folder "Isle of Lore 2" inside
						with Hex Kit!)
./Sources			all sources as Krita (kra) and Photoshop (psd) files
						every overlay and tile type has its own sub-folder
./Sprite Sheets		all the images compiled in sprite sheets
						(use these or single files with Tiled Editor!)
Changelog.txt		logging the asset pack's changes
License.txt			see "License" section above
README.txt			this file
Showcase.kra		example file to show off the assets
Showcase.png		png export
Showcase.psd		psd export

--------------------------------------------------------------------------------
	Specification
--------------------------------------------------------------------------------

> Positions and Dimensions <

Tile File Dimensions		210x210 px
Actual Hex with Border		174x198 px
Actual Hex without Border	145x166 px
Location Spot Position		at 105x113 px

> Color Palette <

White			#efefef
Dark			#69696c
Blue Light		#909b97
Blue Dark		#748481
Brown Light		#d3baac
Brown Dark		#867472
Green Light		#dfdf86
Green Medium	#b6bb7d
Green Dark		#7f8a78
Red				#ff777c
Yellow			#ffda82

> Krita <

The linework is done with the "Ink-2 Fineliner" with a size of 4 px.

--------------------------------------------------------------------------------
	How to Use
--------------------------------------------------------------------------------

> Tiles <

Some of the tiles have a bunch of keywords in their names. They mean the following:

		Sparse		the tile isn't that stuffed with elements and has more open areas
		Dense		the tile is mostly covered with elements
		
		Clear		the tile has a spot at the center to display a location on it
		Covered		the tile has no spot for a location
		
		Green/Blue/Dark		primary color used, so one could make variations, e.g.
							an autumn edition called "Red" for easy discrimination

> Locations <

Like stated above, the tiles with a "clear" in their folder's name can feature
a location drawn on top. If you use locations on a tile without a "clear" in its
name, the location overlaps with elements in front of them (like trees), which
looks weird. The locations come in two ways (which make 4 combinations):

		Framed		the location has a white outline
		Cut			the location doesn't reach into the border of the hex tile
					and looks like it's covered by the border (only noticeable
					with bigger locations, like the "Castle" icon)

> Flairs and Markers <

Overlays allow to add more flavor to a tile and are meant to be displayed on top.

		Flair		additional information displayed at the bottom of a tile
		Marker		location markers with additional information, displayed
					near the top of a tile

> Connections (Roads etc.) <

They are also meant to be placed on top of a tile. If you have a look, the files
have a naming convention with the letters c (center), n (north), e (east),
s (south) and w (west). The number at the end indicates an additional variation.
Therefore, "overlay_river_c_sw.png" is a river from the tile's center to the
south west border of the tile (or vice versa).

--------------------------------------------------------------------------------
	How to Use: Hex Kit
--------------------------------------------------------------------------------

> Adding the Tiles to Hex Kit <

1.	Open Hex Kit
2.	Menu: File > Import Tiles
3.	Go inside the "./Images Legible/Isle of Lore 2" folder and confirm
	(do not go into any sub folders!)
4.	Don't click anything in the appearing "Import Settings" window, except
	the "Save" button (even if it looks weird!)
5.	In the tool window, where the "TILES" are displayed, there's a drop down
	menu with the "HK-Classic", Hex Kit's standard tiles, selected. Select
	"Isle of Lore 2" there.
	
Tip: You don't have to paint by selecting single tiles. You can also
just select a tile group and paint with that (random tiles from the group
will be chosen).

> Using Overlays like Locations <

To paint connections like rivers and roads as well as overlays like markers,
flairs or locations, create new layers for them inside Hex Kit. Tile groups
with a "Clear" in their name allow to display a location on the tile without
weird overlaps.

Tip: Use the locations from the group "Framed" and "Cut". "Framed" means they
have a white outline which makes them easier to spot on a map. "Cut" is best
for Hex Kit, as the locations won't overlap with a tile's border, which is
relevant for big locations like the castle.

--------------------------------------------------------------------------------
	How to Use: Tiled Editor
--------------------------------------------------------------------------------

> Adding the Tiles to Tiled Editor <

1.	Open Tiled
2.	Menu: File > New
3.	Create a new map with the following properties:
	Orientation: Hexagonal (Staggered)
	Tile Size: 160 width and 270 height
4.	To the right, there are two view sections, a top one with the tabs "Minimap",
	"Objects" and "Layers" as well as a bottom one with the tabs "Terrains" and
	"Tilesets". Activate "Tilesets" and drag&drop one of the sprite sheets from
	the asset pack's "Sprite Sheets" folder, e.g.
	"tiles_grassland_sparse_clear_green.png".
	Set the tile size here to 210 width and 210 height, then confirm the import.
	They should show up in the "Tilesets" view and you can select one to paint
	the canvas.

Tip: to paint with random tiles, select multiple of them in the "Tilesets" window
and click on the "two dice" icon in the toolbar to activate the random mode.

> Using Overlays like Locations <

To add a new layer for the overlays like locations, add one in the "Layers"
view. Import the locations like you did with the tiles in step 4. Again, the
tile size should be 210 width and 210 height here.

Tip: Use the locations from the group "Framed" and "Cut". "Framed" means they
have a white outline which makes them easier to spot on a map. "Cut" is best
for Tiled, as the locations won't overlap with a tile's border, which is
relevant for big locations like the castle.

--------------------------------------------------------------------------------
	How to Use: Source Files
--------------------------------------------------------------------------------

A tile's source file has 4 layer groups in it:

	BORDER			the tile's border
	HELPER			additional helper layers (more in a second)
	TILE			the actual tile with its ground and elements
	BACKGROUND		a white background used while working
	
When exporting, make sure to hide HELPER and BACKGROUND.

The HELPER group often has a "location area" layer in it. It's a box indicating
what maximal size the location icons come in. So if you are designing a tile
which should allow a location in it (see the tag "clear" in the section
"How to Use" above), you should make sure that no elements (like trees)
cross over the location area box line. Please be aware that the bottom of the
box extends to the right and left a bit. If you open the source file for
"tile_forest_deciduous_dense_clear_green_0.kra" and activate the visibility for
the "location area" layer, you see how it works: no line of a tree, bush or grass
element crosses the outline of the box. Otherwise, a location would overlap
the element in a weird way!

In the TILE group, you'll find the following:

	MASK				the tile's mask (more in a second)
	OBJECT ELEMENTS		all object elements like bushes, trees or rocks
	GROUND ELEMENTS		all ground elements like grass patches or marks
	ground				the actual ground layer of the tile

The mask, if turned on, hides everything outside the tile's border. Otherwise,
the elements like trees would reach out below the border. If you check a file
like "tile_forest_deciduous_dense_clear_green_0.kra" and turn off the mask layer,
you can see that happen. It's just a comfortable way of creating a tile without
having to erase overlapping parts manually.

--------------------------------------------------------------------------------
	Krita
--------------------------------------------------------------------------------

The files were originally made with the free (and amazing) painting software
Krita, available at krita.org. There are also Photoshop exports available. If
you encounter a problem with those exports, please let me know!

--------------------------------------------------------------------------------
	Modifications
--------------------------------------------------------------------------------

Warning: if you make changes to the files, please copy the files to a different
location first, before you download a new version of the assets and hence
accidentally overwrite your modifications.





Thank you!