DROP DATABASE PathfinderTracker
CREATE DATABASE PathfinderTracker
USE PathfinderTracker
GO

CREATE TABLE Alignments(
	AlignmentID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	Abbreviation nvarchar(10) NOT NULL,
	Description nvarchar(max) NOT NULL,
)

CREATE TABLE ArmorTypes(
	ArmorTypeID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
)

CREATE TABLE Campaigns(
	CampaignID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
)

CREATE TABLE Bloodlines(
	BloodlineID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	Description nvarchar(max) NOT NULL,
)

CREATE TABLE [Domains](
	DomainID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	Description nvarchar(max) NOT NULL,
)

CREATE TABLE CharacterClasses(
	CharacterClassID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	HasBloodline bit NOT NULL,
	HasDomain bit NOT NULL,
	HasMagicSchool bit NOT NULL,
)

CREATE TABLE DamageTypes(
	DamageTypeID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
)

CREATE TABLE Items(
	ItemID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	Description nvarchar(max) NOT NULL,
	ConstructionRequirements nvarchar(max) NOT NULL,
	GPValue int NOT NULL,
	SlotID int NOT NULL,
)


CREATE TABLE Slots(
	SlotID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
)

CREATE TABLE Conditions(
	ConditionID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	Effect nvarchar(45) NOT NULL,
)

CREATE TABLE Deities(
	DeityID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	Description nvarchar(max) NOT NULL,
	AlignmentID int NOT NULL,
)

CREATE TABLE FeatTypes(
	FeatTypeID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
)

CREATE TABLE Feats(
	FeatID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	Prerequisites nvarchar(500) NOT NULL,
	FeatTypeID int NOT NULL,
	CONSTRAINT FeatTypeToFeat FOREIGN KEY(FeatTypeID)
	REFERENCES FeatTypes (FeatTypeID),
	Description nvarchar(max) NOT NULL,
)

CREATE TABLE MagicSchools(
	MagicSchoolID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	Description nvarchar(max) NOT NULL,
)

CREATE TABLE Materials(
	MaterialID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	AmmoAddedGold int NOT NULL,
	LightAddedGold int NOT NULL,
	MediumAddedGold int NOT NULL,
	HeavyAddedGold int NOT NULL,
	WeaponAddedGold int NOT NULL,
	ShieldAddedGold int NOT NULL,
	WeightGoldMultiplier int NOT NULL,
	BaseGoldMultiplier int NOT NULL,
)

CREATE TABLE Races(
	RaceID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
)

CREATE TABLE Players(
	PlayerID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
)

CREATE TABLE Characters(
	CharacterID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	Level int NOT NULL,
	HPMax int NOT NULL,
	HPCurrent int NOT NULL,
	Bonuses nvarchar(max) NOT NULL,
	IsNPC bit NOT NULL,
	PlayerID int NOT NULL,
	DeityID int NOT NULL,
	AlignmentID int NOT NULL,
	CONSTRAINT AlignmentToCharacter FOREIGN KEY(AlignmentID)
	REFERENCES Alignments (AlignmentID),
	RaceID int NOT NULL,
	CONSTRAINT RaceToCharacter FOREIGN KEY(RaceID)
	REFERENCES Races (RaceID),
	CampaignID int NOT NULL,
	CONSTRAINT CampaignToCharacter FOREIGN KEY(CampaignID)
	REFERENCES Campaigns (CampaignID)
)

CREATE TABLE ClassesToCharacters(
	ClassesToCharacterID int IDENTITY(1,1) PRIMARY KEY,
	ClassLevel int NOT NULL,
	CharacterID int NOT NULL,
	CONSTRAINT CharacterToClass FOREIGN KEY(CharacterID)
	REFERENCES Characters (CharacterID),
	CharacterClassID int NOT NULL,
	CONSTRAINT ClassToCharacter FOREIGN KEY(CharacterClassID)
	REFERENCES CharacterClasses (CharacterClassID),
	BloodlineID int NOT NULL,
	DomainID int NOT NULL,
	MagicSchoolID int NOT NULL,
)

CREATE TABLE Spells(
	SpellID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	Description nvarchar(max) NOT NULL,
	MagicSchoolID int NOT NULL,
	CONSTRAINT MagicSchoolToSpell FOREIGN KEY(MagicSchoolID)
	REFERENCES MagicSchools (MagicSchoolID),
	CastingTime nvarchar(45) NOT NULL,
	RangeDistance nvarchar(45) NOT NULL,
	Target nvarchar(45) NOT NULL,
	Duration nvarchar(45) NOT NULL,
	SavingThrow nvarchar(45) NOT NULL,
	SpellResistance nvarchar(45) NOT NULL,
)

CREATE TABLE SpellsToCharacters(
	SpellsToCharacterID int IDENTITY(1,1) PRIMARY KEY,
	CharacterID int NOT NULL,
	CONSTRAINT CharacterToSpell FOREIGN KEY(CharacterID)
	REFERENCES Characters (CharacterID),
	SpellID int NOT NULL,
	CONSTRAINT SpellToCharacter FOREIGN KEY(SpellID)
	REFERENCES Spells (SpellID)
)

CREATE TABLE WeaponCategories(
	WeaponCategoryID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
)

CREATE TABLE WeaponCoreTypes(
	WeaponCoreTypeID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
)

CREATE TABLE ArmorCoreTypes(
	ArmorCoreTypeID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
)

CREATE TABLE WeaponTypes(
	WeaponTypeID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	WeaponCoreTypeID int NOT NULL,
	CONSTRAINT WeaponCoreTypeToWeapon FOREIGN KEY(WeaponCoreTypeID)
	REFERENCES WeaponCoreTypes (WeaponCoreTypeID),
	Critical nvarchar(45) NOT NULL,
	Weight int NOT NULL,
	AttackRange int NOT NULL,
	GPValue int NOT NULL,
	AttackDiceSmall nvarchar(45) NOT NULL,
	AttackDiceMedium nvarchar(45) NOT NULL,
	WeaponCategoryID int NOT NULL,
	CONSTRAINT WeaponCategoryToWeapon FOREIGN KEY(WeaponCategoryID)
	REFERENCES WeaponCategories (WeaponCategoryID),
)

CREATE TABLE ArmorAddons(
	ArmorAddonID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	GPValue int NOT NULL,
	ArmorCheckPenalty int NOT NULL,
	Weight int NOT NULL,
	MaterialID int NOT NULL,
	CONSTRAINT MaterialToArmorAddon FOREIGN KEY(MaterialID)
	REFERENCES Materials (MaterialID)
)

CREATE TABLE Weapons(
	WeaponID int IDENTITY(1,1) PRIMARY KEY,
	SpecialAttributes nvarchar(255) NOT NULL,
	WeaponTypeID int NOT NULL,
	CONSTRAINT WeaponTypeToWeapon FOREIGN KEY(WeaponTypeID)
	REFERENCES WeaponTypes (WeaponTypeID),
	MaterialID int NOT NULL,
	CONSTRAINT MaterialToWeapon FOREIGN KEY(MaterialID)
	REFERENCES Materials (MaterialID)
)

CREATE TABLE Armors(
	ArmorID int IDENTITY(1,1) PRIMARY KEY,
	SpecialAttributes nvarchar(255) NOT NULL,
	BaseGPValue int NOT NULL,
	ACBonus int NOT NULL,
	ArmorCheckPenalty int NOT NULL,
	ArcaneSpellFailureChance int NOT NULL,
	ArmorAddonID int NOT NULL,
	Weight int NOT NULL,
	ArmorTypeID int NOT NULL,
	CONSTRAINT ArmorTypeToArmor FOREIGN KEY(ArmorTypeID)
	REFERENCES ArmorTypes (ArmorTypeID),
	ArmorCoreTypeID int NOT NULL,
	CONSTRAINT ArmorCoreTypeToArmor FOREIGN KEY(ArmorCoreTypeID)
	REFERENCES ArmorCoreTypes (ArmorCoreTypeID),
	MaterialID int NOT NULL,
	CONSTRAINT MaterialToArmor FOREIGN KEY(MaterialID)
	REFERENCES Materials (MaterialID)
)

CREATE TABLE WeaponsToCharacters(
	WeaponsToCharacterID int IDENTITY(1,1) PRIMARY KEY,
	CharacterID int NOT NULL,
	CONSTRAINT CharacterToWeapon FOREIGN KEY(CharacterID)
	REFERENCES Characters (CharacterID),
	WeaponID int NOT NULL,
	CONSTRAINT WeaponToCharacter FOREIGN KEY(WeaponID)
	REFERENCES Weapons (WeaponID)
)

CREATE TABLE ArmorsToCharacters(
	ArmorsToCharacterID int IDENTITY(1,1) PRIMARY KEY,
	CharacterID int NOT NULL,
	CONSTRAINT CharacterToArmor FOREIGN KEY(CharacterID)
	REFERENCES Characters (CharacterID),
	ArmorID int NOT NULL,
	CONSTRAINT ArmorToCharacter FOREIGN KEY(ArmorID)
	REFERENCES Armors (ArmorID)
)
GO

INSERT INTO Alignments (Name, Abbreviation, Description) VALUES 
	('Lawful Good', 'LG', 'A lawful good character acts as a good person is expected or required to act. She combines a commitment to oppose evil with the discipline to fight relentlessly. She tells the truth, keeps her word, helps those in need, and speaks out against injustice. A lawful good character hates to see the guilty go unpunished.'),	
	('Neutral Good', 'NG', 'A neutral good character does the best that a good person can do. He is devoted to helping others. He works with kings and magistrates but does not feel beholden to them. Neutral good means doing what is good and right without bias for or against order.'),	
	('Chaotic Good', 'CG', 'A chaotic good character acts as his conscience directs him with little regard for what others expect of him. He makes his own way, but he’s kind and benevolent. He believes in goodness and right but has little use for laws and regulations. He hates it when people try to intimidate others and tell them what to do. He follows his own moral compass, which, although good, may not agree with that of society.'),	
	('Lawful Neutral', 'LN', 'A lawful neutral character acts as law, tradition, or a personal code directs her. Order and organization are paramount. She may believe in personal order and live by a code or standard, or she may believe in order for all and favor a strong, organized government.'),	
	('Neutral', 'N', 'A neutral character does what seems to be a good idea. She doesn’t feel strongly one way or the other when it comes to good vs. evil or law vs. chaos (and thus neutral is sometimes called “true neutral”). Most neutral characters exhibit a lack of conviction or bias rather than a commitment to neutrality. Such a character probably thinks of good as better than evil—after all, she would rather have good neighbors and rulers than evil ones. Still, she’s not personally committed to upholding good in any abstract or universal way.'),	
	('Chaotic Neutral', 'CN', 'A chaotic neutral character follows his whims. He is an individualist first and last. He values his own liberty but doesn’t strive to protect others’ freedom. He avoids authority, resents restrictions, and challenges traditions. A chaotic neutral character does not intentionally disrupt organizations as part of a campaign of anarchy. To do so, he would have to be motivated either by good (and a desire to liberate others) or evil (and a desire to make those others suffer). a chaotic neutral character may be unpredictable, but his behavior is not totally random. He is not as likely to jump off a bridge as he is to cross it.'),	
	('Lawful Evil', 'LE', 'A lawful evil villain methodically takes what he wants within the limits of his code of conduct without regard for whom it hurts. He cares about tradition, loyalty, and order, but not about freedom, dignity, or life. He plays by the rules but without mercy or compassion. He is comfortable in a hierarchy and would like to rule, but is willing to serve. He condemns others not according to their actions but according to race, religion, homeland, or social rank. He is loath to break laws or promises.'),	
	('Neutral Evil', 'NE', 'A neutral evil villain does whatever she can get away with. She is out for herself, pure and simple. She sheds no tears for those she kills, whether for profit, sport, or convenience. She has no love of order and holds no illusions that following laws, traditions, or codes would make her any better or more noble. On the other hand, she doesn’t have the restless nature or love of conflict that a chaotic evil villain has.'),	
	('Chaotic Evil', 'CE', 'A chaotic evil character does what his greed, hatred, and lust for destruction drive him to do. He is vicious, arbitrarily violent, and unpredictable. If he is simply out for whatever he can get, he is ruthless and brutal. If he is committed to the spread of evil and chaos, he is even worse. Thankfully, his plans are haphazard, and any groups he joins or forms are likely to be poorly organized. Typically, chaotic evil people can be made to work together only by force, and their leader lasts only as long as he can thwart attempts to topple or assassinate him.')

INSERT INTO ArmorTypes(Name) VALUES 
	('Plate'),('Padded'),('Studded')

INSERT INTO WeaponCategories(Name) VALUES 
	('Simple'),('Martial'),('Exotic')

INSERT INTO WeaponCoreTypes(Name) VALUES 
	('One-Handed Melee'),('Two-Handed Melee'),('One-Handed Ranged'),('Two-Handed Ranged')

INSERT INTO ArmorCoreTypes(Name) VALUES 
	('Light'),('Medium'),('Heavy'),('Shield')

INSERT INTO Materials(Name, AmmoAddedGold, LightAddedGold, MediumAddedGold, HeavyAddedGold, WeaponAddedGold, ShieldAddedGold, BaseGoldMultiplier, WeightGoldMultiplier) VALUES 
	('Adamantine', 60, 5000, 10000, 15000, 3000, 3000, 1, 1),('Angelskin', 0, 1000, 2000, 0, 0, 0, 1, 1),('Aszite', 0, 750, 750, 1000, 0, 0, 1, 1),
	('Cryptstone', 10, 0, 0, 0, 500, 0, 1, 1),('Blood Crystal', 30, 0, 0, 0, 1500, 0, 1, 1),('Darkleaf ', 0, 750, 1500, 0, 0, 0, 1, 1),
	('Darkwood ', 0, 0, 0, 0, 0, 0, 1, 10),('Dragonhide ', 0, 0, 0, 0, 0, 0, 2, 1),('Dragonskin ', 0, 1500, 3000, 4500, 0, 3000, 1, 1),
	('Druchite ', 12, 1000, 1500, 2000, 1200, 0, 1, 1),('Eel Hide ', 0, 1200, 1800, 0, 0, 0, 1, 1),('Elysian Bronze', 20, 1000, 2000, 3000, 1000, 0, 1, 1),
	('Mithral', 0, 1000, 4000, 9000, 500, 1000, 1, 1),('Iron', 0, 0, 0, 0, 0, 0, 1, 1),('Cold Iron', 0, 0, 0, 0, 2000, 0, 2, 1),
	('Greenwood', 0, 0, 0, 0, 0, 0, 1, 50),('Inubrix', 0, 0, 0, 0, 5000, 0, 1, 1)

GO

CREATE PROCEDURE sproc_AlignmentAdd
@AlignmentID int OUTPUT, 
@Name nvarchar(45),
@Abbreviation nvarchar(10),
@Description nvarchar(max)
AS
BEGIN
  INSERT INTO Alignments (Name, Abbreviation, Description) 
  VALUES (@Name, @Abbreviation, @Description);
  SET @AlignmentID = @@IDENTITY;
END
GO

CREATE PROCEDURE sproc_AlignmentUpdate
@AlignmentID int, 
@Name nvarchar(45),
@Abbreviation nvarchar(10),
@Description nvarchar(max)
AS
BEGIN
	UPDATE Alignments SET 
	Name = @Name, 
	Abbreviation = @Abbreviation, 
	Description = @Description
		WHERE AlignmentID = @AlignmentID
END
GO


CREATE PROCEDURE sproc_AlignmentDelete
@AlignmentID int  
AS
BEGIN
  DELETE FROM Alignments
  WHERE AlignmentID = @AlignmentID;
END
GO


CREATE PROCEDURE sproc_ArmorAdd
@ArmorID int OUTPUT,
@SpecialAttributes nvarchar(255), 
@ACBonus int,
@BaseGPValue int,
@Weight int,
@ArmorTypeID int,
@ArmorCoreTypeID int,
@ArmorAddonID int, 
@ArmorCheckPenalty int,
@ArcaneSpellFailureChance int,
@MaterialID int
AS
BEGIN
  INSERT INTO Armors (SpecialAttributes, ACBonus, BaseGPValue, Weight, ArmorCoreTypeID,
	ArmorCheckPenalty, ArcaneSpellFailureChance, MaterialID, ArmorTypeID, ArmorAddonID) 
  VALUES (@SpecialAttributes, @ACBonus, @BaseGPValue, @Weight, @ArmorCoreTypeID,
	@ArmorCheckPenalty, @ArcaneSpellFailureChance, @MaterialID, @ArmorTypeID, @ArmorAddonID);
  SET @ArmorID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_ArmorUpdate
@ArmorID int, 
@SpecialAttributes nvarchar(255), 
@ACBonus int,
@BaseGPValue int,
@Weight int,
@ArmorTypeID int,
@ArmorCoreTypeID int,
@ArmorAddonID int, 
@ArmorCheckPenalty int,
@ArcaneSpellFailureChance int,
@MaterialID int
AS
BEGIN
	UPDATE Armors	SET 
	SpecialAttributes = @SpecialAttributes,
	ACBonus = @ACBonus,
	BaseGPValue = @BaseGPValue,
	Weight = @Weight,
	ArmorTypeID = @ArmorTypeID,
	ArmorCoreTypeID = @ArmorCoreTypeID,
	ArmorAddonID = @ArmorAddonID,
	ArmorCheckPenalty = @ArmorCheckPenalty,
	ArcaneSpellFailureChance = @ArcaneSpellFailureChance,
	MaterialID = @MaterialID
		WHERE ArmorID = @ArmorID
END
GO


CREATE PROCEDURE sproc_ArmorAddonAdd
@ArmorAddonID int OUTPUT,
@Name nvarchar(45),
@GPValue int,
@ArmorCheckPenalty int,
@Weight int, 
@MaterialID int
AS
BEGIN
  INSERT INTO ArmorAddons (Name, GPValue, ArmorCheckPenalty, Weight, MaterialID) 
  VALUES (@Name, @GPValue, @ArmorCheckPenalty, @Weight, @MaterialID);
  SET @ArmorAddonID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_ArmorAddonUpdate
@ArmorAddonID int,
@Name nvarchar(45),
@GPValue int,
@ArmorCheckPenalty int,
@Weight int, 
@MaterialID int
AS
BEGIN
	UPDATE ArmorAddons SET
	Name = @Name,
	GPValue = @GPValue,
	ArmorCheckPenalty = @ArmorCheckPenalty,
	Weight = @Weight,
	MaterialID = @MaterialID
		WHERE ArmorAddonID = @ArmorAddonID
END
GO


CREATE PROCEDURE sproc_ArmorAddOnDelete
@ArmorAddOnID int  
AS
BEGIN
  DELETE FROM ArmorAddOns
  WHERE ArmorAddOnID = @ArmorAddOnID;
END
GO


CREATE PROCEDURE sproc_ArmorDelete
@ArmorID int  
AS
BEGIN
  DELETE FROM Armors
  WHERE ArmorID = @ArmorID;
END
GO


CREATE PROCEDURE sproc_ArmorsToCharacterAdd
@ArmorsToCharacterID int OUTPUT, 
@CharacterID int,
@ArmorID int
AS
BEGIN
  INSERT INTO ArmorsToCharacters (CharacterID, ArmorID) 
  VALUES (@CharacterID, @ArmorID);
  SET @ArmorsToCharacterID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_ArmorsToCharacterUpdate
@ArmorsToCharacterID int, 
@CharacterID int,
@ArmorID int
AS
BEGIN
	UPDATE ArmorsToCharacters SET
	CharacterID = @CharacterID,
	ArmorID = @ArmorID
		WHERE ArmorsToCharacterID = @ArmorsToCharacterID
END
GO


CREATE PROCEDURE sproc_ArmorToCharacterDelete
@ArmorsToCharacterID int  
AS
BEGIN
  DELETE FROM ArmorsToCharacters
  WHERE ArmorsToCharacterID = @ArmorsToCharacterID;
END
GO


CREATE PROCEDURE sproc_ArmorTypeAdd
@ArmorTypeID int OUTPUT, 
@Name nvarchar(45)
AS
BEGIN
  INSERT INTO ArmorTypes (Name) 
  VALUES (@Name);
  SET @ArmorTypeID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_ArmorTypeUpdate
@ArmorTypeID int, 
@Name nvarchar(45)
AS
BEGIN
	UPDATE ArmorTypes SET
	Name = @Name
		WHERE ArmorTypeID = @ArmorTypeID
END
GO


CREATE PROCEDURE sproc_ArmorTypeDelete
@ArmorTypeID int  
AS
BEGIN
  DELETE FROM ArmorTypes
  WHERE ArmorTypeID = @ArmorTypeID;
END
GO


CREATE PROCEDURE sproc_CampaignAdd
@CampaignID int OUTPUT,
@Name nvarchar(45)
AS
BEGIN
  INSERT INTO Campaigns (Name) 
  VALUES (@Name);
  SET @CampaignID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_CampaignUpdate
@CampaignID int,
@Name nvarchar(45)
AS
BEGIN
	UPDATE Campaigns SET
	Name = @Name
		WHERE CampaignID = @CampaignID
END
GO


CREATE PROCEDURE sproc_CampaignDelete
@CampaignID int  
AS
BEGIN
  DELETE FROM Campaigns
  WHERE CampaignID = @CampaignID;
END
GO


CREATE PROCEDURE sproc_CharacterAdd
@CharacterID int OUTPUT, 
@Name nvarchar(45),
@Level int,
@IsNPC bit,
@AlignmentID int,
@DeityID int,
@HPMax int,
@HPCurrent int,
@Bonuses nvarchar(max),
@RaceID int,
@CampaignID int,
@PlayerID int
AS
BEGIN
  INSERT INTO Characters (Name, Level, IsNPC, AlignmentID, DeityID, HPMax, HPCurrent, Bonuses, RaceID, CampaignID, PlayerID) 
  VALUES (@Name, @Level, @IsNPC, @AlignmentID, @DeityID, @HPMax, @HPCurrent, @Bonuses, @RaceID, @CampaignID, @PlayerID);
  SET @CharacterID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_CharacterUpdate
@CharacterID int, 
@Name nvarchar(45),
@Level int,
@IsNPC bit,
@AlignmentID int,
@DeityID int,
@HPMax int,
@HPCurrent int,
@Bonuses nvarchar(max),
@RaceID int,
@CampaignID int,
@PlayerID int
AS
BEGIN
	UPDATE Characters SET
	Name = @Name,
	Level = @Level,
	IsNPC = @IsNPC,
	AlignmentID = @AlignmentID,
	DeityID = @DeityID,
	HPMax = @HPMax,
	HPCurrent = @HPCurrent,
	Bonuses = @Bonuses,
	RaceID = @RaceID,
	CampaignID = @CampaignID,
	PlayerID = @PlayerID
		WHERE CharacterID = @CharacterID
END
GO


CREATE PROCEDURE sproc_CharacterDelete
@CharacterID int  
AS
BEGIN
  DELETE FROM Characters
  WHERE CharacterID = @CharacterID;
END
GO


CREATE PROCEDURE sproc_CharacterClassAdd
@CharacterClassID int OUTPUT, 
@Name nvarchar(45),
@HasBloodline bit,
@HasDomain bit,
@HasMagicSchool bit
AS
BEGIN
  INSERT INTO CharacterClasses (Name, HasBloodline, HasDomain, HasMagicSchool) 
  VALUES (@Name, @HasBloodline, @HasDomain, @HasMagicSchool);
  SET @CharacterClassID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_CharacterClassUpdate
@CharacterClassID int, 
@Name nvarchar(45),
@HasBloodline bit,
@HasDomain bit,
@HasMagicSchool bit
AS
BEGIN
	UPDATE CharacterClasses SET
	Name = @Name,
	HasBloodline = @HasBloodline,
	HasDomain = @HasDomain,
	HasMagicSchool = @HasMagicSchool
		WHERE CharacterClassID = @CharacterClassID
END
GO


CREATE PROCEDURE sproc_CharacterClassDelete
@CharacterClassID int  
AS
BEGIN
  DELETE FROM CharacterClasses
  WHERE CharacterClassID = @CharacterClassID;
END
GO

CREATE PROCEDURE sproc_ClassesToCharacterAdd
@ClassesToCharacterID int OUTPUT, 
@ClassLevel int,
@CharacterID int,
@CharacterClassID int,
@BloodlineID int,
@DomainID int,
@MagicSchoolID int
AS
BEGIN
  INSERT INTO ClassesToCharacters (ClassLevel, CharacterID, CharacterClassID, BloodlineID, DomainID, MagicSchoolID) 
  VALUES (@ClassLevel, @CharacterID, @CharacterClassID, @BloodlineID, @DomainID, @MagicSchoolID);
  SET @ClassesToCharacterID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_ClassesToCharacterUpdate
@ClassesToCharacterID int, 
@ClassLevel int,
@CharacterID int,
@CharacterClassID int,
@BloodlineID int,
@DomainID int,
@MagicSchoolID int
AS
BEGIN
	UPDATE ClassesToCharacters SET
	ClassLevel = @ClassLevel,
	CharacterID = @CharacterID,
	CharacterClassID = @CharacterClassID,
	BloodlineID = @BloodlineID,
	DomainID = @DomainID,
	MagicSchoolID = @MagicSchoolID
		WHERE ClassesToCharacterID = @ClassesToCharacterID
END
GO


CREATE PROCEDURE sproc_ClassesToCharacterDelete
@ClassesToCharacterID int  
AS
BEGIN
  DELETE FROM ClassesToCharacters
  WHERE ClassesToCharacterID = @ClassesToCharacterID;
END
GO


CREATE PROCEDURE sproc_DamageTypeAdd
@DamageTypeID int OUTPUT, 
@Name nvarchar(45)
AS
BEGIN
  INSERT INTO DamageTypes (Name) 
  VALUES (@Name);
  SET @DamageTypeID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_DamageTypeUpdate
@DamageTypeID int, 
@Name nvarchar(45)
AS
BEGIN
	UPDATE DamageTypes SET
	Name = @Name
		WHERE DamageTypeID = @DamageTypeID
END
GO


CREATE PROCEDURE sproc_DamageTypeDelete
@DamageTypeID int  
AS
BEGIN
  DELETE FROM DamageTypes
  WHERE DamageTypeID = @DamageTypeID;
END
GO

CREATE PROCEDURE sproc_ConditionAdd
@ConditionID int OUTPUT, 
@Name nvarchar(45),
@Effect nvarchar(45)
AS
BEGIN
  INSERT INTO Conditions (Name, Effect) 
  VALUES (@Name, @Effect);
  SET @ConditionID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_ConditionUpdate
@ConditionID int, 
@Name nvarchar(45),
@Effect nvarchar(45)
AS
BEGIN
	UPDATE Conditions SET
	Name = @Name,
	Effect = @Effect
		WHERE ConditionID = @ConditionID
END
GO


CREATE PROCEDURE sproc_ConditionDelete
@ConditionID int  
AS
BEGIN
  DELETE FROM Conditions
  WHERE ConditionID = @ConditionID;
END
GO


CREATE PROCEDURE sproc_DeityAdd
@DeityID int OUTPUT, 
@Name nvarchar(45),
@AlignmentID int,
@Description nvarchar(max)
AS
BEGIN
  INSERT INTO Deities (Name, AlignmentID, Description) 
  VALUES (@Name, @AlignmentID, @Description);
  SET @DeityID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_DeityUpdate
@DeityID int, 
@Name nvarchar(45),
@AlignmentID int,
@Description nvarchar(max)
AS
BEGIN
	UPDATE Deities SET
	Name = @Name,
	AlignmentID = @AlignmentID,
	Description = @Description
		WHERE DeityID = @DeityID
END
GO


CREATE PROCEDURE sproc_DeityDelete
@DeityID int  
AS
BEGIN
  DELETE FROM Deities
  WHERE DeityID = @DeityID;
END
GO

CREATE PROCEDURE sproc_FeatTypeAdd
@FeatTypeID int OUTPUT,
@Name nvarchar(45)
AS
BEGIN
  INSERT INTO FeatTypes (Name) 
  VALUES (@Name);
  SET @FeatTypeID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_FeatTypeUpdate
@FeatTypeID int,
@Name nvarchar(45)
AS
BEGIN
	UPDATE FeatTypes SET
	Name = @Name
		WHERE FeatTypeID = @FeatTypeID
END
GO


CREATE PROCEDURE sproc_FeatTypeDelete
@FeatTypeID int  
AS
BEGIN
  DELETE FROM FeatTypes
  WHERE FeatTypeID = @FeatTypeID;
END
GO


CREATE PROCEDURE sproc_FeatAdd
@FeatID int OUTPUT,
@Name nvarchar(45),
@Description nvarchar(max),
@Prerequisites nvarchar(500),
@FeatTypeID int
AS
BEGIN
  INSERT INTO Feats (Name, Description, Prerequisites, FeatTypeID) 
  VALUES (@Name, @Description, @Prerequisites, @FeatTypeID);
  SET @FeatID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_FeatUpdate
@FeatID int,
@Name nvarchar(45),
@Description nvarchar(max),
@Prerequisites nvarchar(500),
@FeatTypeID int
AS
BEGIN
	UPDATE Feats SET
	Name = @Name,
	Description = @Description,
	Prerequisites = @Prerequisites,
	FeatTypeID = @FeatTypeID
		WHERE FeatID = @FeatID
END
GO


CREATE PROCEDURE sproc_FeatDelete
@FeatID int  
AS
BEGIN
  DELETE FROM Feats
  WHERE FeatID = @FeatID;
END
GO


CREATE PROCEDURE sproc_ItemAdd
@ItemID int OUTPUT,
@Name nvarchar(45),
@Description nvarchar(max),
@ConstructionRequirements nvarchar(max),
@GPValue int,
@SlotID int
AS
BEGIN
  INSERT INTO Items (Name, Description, ConstructionRequirements, GPValue, SlotID) 
  VALUES (@Name, @Description, @ConstructionRequirements, @GPValue, @SlotID);
  SET @ItemID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_ItemUpdate
@ItemID int,
@Name nvarchar(45),
@Description nvarchar(max),
@ConstructionRequirements nvarchar(max),
@GPValue int,
@SlotID int
AS
BEGIN
	UPDATE Items SET
	Name = @Name,
	Description = @Description,
	ConstructionRequirements = @ConstructionRequirements,
	GPValue = @GPValue,
	SlotID = @SlotID
		WHERE ItemID = @ItemID
END
GO


CREATE PROCEDURE sproc_ItemDelete
@ItemID int  
AS
BEGIN
  DELETE FROM Items
  WHERE ItemID = @ItemID;
END
GO


CREATE PROCEDURE sproc_MagicSchoolAdd
@MagicSchoolID int OUTPUT,
@Name nvarchar(45),
@Description nvarchar(max)
AS
BEGIN
  INSERT INTO MagicSchools (Name, Description) 
  VALUES (@Name, @Description);
  SET @MagicSchoolID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_MagicSchoolUpdate
@MagicSchoolID int,
@Name nvarchar(45),
@Description nvarchar(max)
AS
BEGIN
	UPDATE MagicSchools SET
	Name = @Name,
	Description = @Description
		WHERE MagicSchoolID = @MagicSchoolID
END
GO


CREATE PROCEDURE sproc_MagicSchoolDelete
@MagicSchoolID int  
AS
BEGIN
  DELETE FROM MagicSchools
  WHERE MagicSchoolID = @MagicSchoolID;
END
GO


CREATE PROCEDURE sproc_MaterialAdd
@MaterialID int OUTPUT,
@Name nvarchar(45),
@AmmoAddedGold int,
@LightAddedGold int,
@MediumAddedGold int,
@HeavyAddedGold int,
@WeaponAddedGold int,
@ShieldAddedGold int,
@WeightGoldMultiplier int,
@BaseGoldMultiplier int
AS
BEGIN
  INSERT INTO Materials (Name, AmmoAddedGold, LightAddedGold, MediumAddedGold, HeavyAddedGold, WeaponAddedGold, ShieldAddedGold, WeightGoldMultiplier, BaseGoldMultiplier) 
  VALUES (@Name, @AmmoAddedGold, @LightAddedGold, @MediumAddedGold, @HeavyAddedGold, @WeaponAddedGold, @ShieldAddedGold, @WeightGoldMultiplier, @BaseGoldMultiplier);
  SET @MaterialID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_MaterialUpdate
@MaterialID int,
@Name nvarchar(45),
@AmmoAddedGold int,
@LightAddedGold int,
@MediumAddedGold int,
@HeavyAddedGold int,
@WeaponAddedGold int,
@ShieldAddedGold int,
@WeightGoldMultiplier int,
@BaseGoldMultiplier int
AS
BEGIN
	UPDATE Materials SET
	Name = @Name,
	AmmoAddedGold = @AmmoAddedGold,
	LightAddedGold = @LightAddedGold,
	MediumAddedGold = @MediumAddedGold,
	HeavyAddedGold = @HeavyAddedGold,
	WeaponAddedGold = @WeaponAddedGold,
	ShieldAddedGold = @ShieldAddedGold,
	WeightGoldMultiplier = @WeightGoldMultiplier,
	BaseGoldMultiplier = @BaseGoldMultiplier
		WHERE MaterialID = @MaterialID
END
GO


CREATE PROCEDURE sproc_MaterialDelete
@MaterialID int  
AS
BEGIN
  DELETE FROM Materials
  WHERE MaterialID = @MaterialID;
END
GO


CREATE PROCEDURE sproc_PlayerAdd
@PlayerID int OUTPUT,
@Name nvarchar(45)
AS
BEGIN
  INSERT INTO Players (Name) 
  VALUES (@Name);
  SET @PlayerID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_PlayerUpdate
@PlayerID int,
@Name nvarchar(45)
AS
BEGIN
	UPDATE Players SET
	Name = @Name
		WHERE PlayerID = @PlayerID
END
GO


CREATE PROCEDURE sproc_PlayerDelete
@PlayerID int  
AS
BEGIN
  DELETE FROM Players
  WHERE PlayerID = @PlayerID;
END
GO


CREATE PROCEDURE sproc_RaceAdd
@RaceID int OUTPUT,
@Name nvarchar(45)
AS
BEGIN
  INSERT INTO Races (Name) 
  VALUES (@Name);
  SET @RaceID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_RaceUpdate
@RaceID int,
@Name nvarchar(45)
AS
BEGIN
	UPDATE Races SET
	Name = @Name
		WHERE RaceID = @RaceID
END
GO


CREATE PROCEDURE sproc_RaceDelete
@RaceID int  
AS
BEGIN
  DELETE FROM Races
  WHERE RaceID = @RaceID;
END
GO


CREATE PROCEDURE sproc_SpellAdd
@SpellID int OUTPUT,  
@Name nvarchar(45),
@Description nvarchar(max),
@CastingTime nvarchar(45),
@RangeDistance nvarchar(45),
@Target nvarchar(45),
@Duration nvarchar(45),
@SavingThrow nvarchar(45),
@SpellResistance nvarchar(45),
@MagicSchoolID int
AS
BEGIN
  INSERT INTO Spells (Name, Description, CastingTime, RangeDistance, Target, Duration, SavingThrow, SpellResistance, MagicSchoolID) 
  VALUES (@Name, @Description, @CastingTime, @RangeDistance, @Target, @Duration, @SavingThrow, @SpellResistance, @MagicSchoolID);
  SET @SpellID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_SpellUpdate
@SpellID int,  
@Name nvarchar(45),
@Description nvarchar(max),
@CastingTime nvarchar(45),
@RangeDistance nvarchar(45),
@Target nvarchar(45),
@Duration nvarchar(45),
@SavingThrow nvarchar(45),
@SpellResistance nvarchar(45),
@MagicSchoolID int
AS
BEGIN
	UPDATE Spells SET
	Name = @Name,
	Description = @Description,
	CastingTime = @CastingTime,
	RangeDistance = @RangeDistance,
	Target = @Target,
	Duration = @Duration,
	SavingThrow = @SavingThrow,
	SpellResistance = @SpellResistance,
	MagicSchoolID = @MagicSchoolID
		WHERE SpellID = @SpellID
END
GO


CREATE PROCEDURE sproc_SpellDelete
@SpellID int  
AS
BEGIN
  DELETE FROM Spells
  WHERE SpellID = @SpellID;
END
GO


CREATE PROCEDURE sproc_SpellsToCharacterAdd
@SpellsToCharacterID int OUTPUT,
@CharacterID int,
@SpellID int
AS
BEGIN
  INSERT INTO SpellsToCharacters (CharacterID, SpellID) 
  VALUES (@CharacterID, @SpellID);
  SET @SpellsToCharacterID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_SpellsToCharacterUpdate
@SpellsToCharacterID int,
@CharacterID int,
@SpellID int
AS
BEGIN
	UPDATE SpellsToCharacters SET
	CharacterID = @CharacterID,
	SpellID = @SpellID
		WHERE SpellsToCharacterID = @SpellsToCharacterID
END
GO


CREATE PROCEDURE sproc_SpellsToCharacterDelete
@SpellsToCharacterID int  
AS
BEGIN
  DELETE FROM SpellsToCharacters
  WHERE SpellsToCharacterID = @SpellsToCharacterID;
END
GO


CREATE PROCEDURE sproc_SlotAdd
@SlotID int OUTPUT,
@Name nvarchar(45)
AS
BEGIN
  INSERT INTO Slots (Name) 
  VALUES (@Name);
  SET @SlotID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_SlotUpdate
@SlotID int,
@Name nvarchar(45)
AS
BEGIN
	UPDATE Slots SET
	Name = @Name
		WHERE SlotID = @SlotID
END
GO


CREATE PROCEDURE sproc_SlotDelete
@SlotID int  
AS
BEGIN
  DELETE FROM Slots
  WHERE SlotID = @SlotID;
END
GO


CREATE PROCEDURE sproc_BloodlineAdd
@BloodlineID int OUTPUT,  
@Name nvarchar(45),
@Description nvarchar(max)
AS
BEGIN
  INSERT INTO Bloodlines (Name, Description) 
  VALUES (@Name, @Description);
  SET @BloodlineID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_BloodlineUpdate
@BloodlineID int,  
@Name nvarchar(45),
@Description nvarchar(max)
AS
BEGIN
	UPDATE Bloodlines SET
	Name = @Name,
	Description = @Description
		WHERE BloodlineID = @BloodlineID
END
GO


CREATE PROCEDURE sproc_BloodlineDelete
@BloodlineID int  
AS
BEGIN
  DELETE FROM Bloodlines
  WHERE BloodlineID = @BloodlineID;
END
GO


CREATE PROCEDURE sproc_DomainAdd
@DomainID int OUTPUT,  
@Name nvarchar(45),
@Description nvarchar(max)
AS
BEGIN
  INSERT INTO [Domains] (Name, Description) 
  VALUES (@Name, @Description);
  SET @DomainID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_DomainUpdate
@DomainID int,  
@Name nvarchar(45),
@Description nvarchar(max)
AS
BEGIN
	UPDATE [Domains] SET
	Name = @Name,
	Description = @Description
		WHERE DomainID = @DomainID
END
GO


CREATE PROCEDURE sproc_DomainDelete
@DomainID int  
AS
BEGIN
  DELETE FROM [Domains]
  WHERE DomainID = @DomainID;
END
GO


CREATE PROCEDURE sproc_WeaponCoreTypeAdd
@WeaponCoreTypeID int OUTPUT,
@Name nvarchar(45)
AS
BEGIN
  INSERT INTO WeaponCoreTypes (Name) 
  VALUES (@Name);
  SET @WeaponCoreTypeID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_WeaponCoreTypeUpdate
@WeaponCoreTypeID int,
@Name nvarchar(45)
AS
BEGIN
	UPDATE WeaponCoreTypes SET
	Name = @Name
		WHERE WeaponCoreTypeID = @WeaponCoreTypeID
END
GO


CREATE PROCEDURE sproc_WeaponCoreTypeDelete
@WeaponCoreTypeID int  
AS
BEGIN
  DELETE FROM WeaponCoreTypes
  WHERE WeaponCoreTypeID = @WeaponCoreTypeID;
END
GO


CREATE PROCEDURE sproc_WeaponAdd
@WeaponID int OUTPUT,
@SpecialAttributes nvarchar(255),
@WeaponTypeID int,
@MaterialID int
AS
BEGIN
  INSERT INTO Weapons (SpecialAttributes, WeaponTypeID, MaterialID) 
  VALUES (@SpecialAttributes, @WeaponTypeID, @MaterialID);
  SET @WeaponID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_WeaponUpdate
@WeaponID int,
@SpecialAttributes nvarchar(255),
@WeaponTypeID int,
@MaterialID int
AS
BEGIN
	UPDATE Weapons SET
	SpecialAttributes = @SpecialAttributes,
	WeaponTypeID = @WeaponTypeID,
	MaterialID = @MaterialID
		WHERE WeaponID = @WeaponID
END
GO


CREATE PROCEDURE sproc_WeaponDelete
@WeaponID int  
AS
BEGIN
  DELETE FROM Weapons
  WHERE WeaponID = @WeaponID;
END
GO


CREATE PROCEDURE sproc_WeaponsToCharacterAdd
@WeaponsToCharacterID int OUTPUT,  
@CharacterID int,
@WeaponID int
AS
BEGIN
  INSERT INTO WeaponsToCharacters (CharacterID, WeaponID) 
  VALUES (@CharacterID, @WeaponID);
  SET @WeaponsToCharacterID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_WeaponsToCharacterUpdate
@WeaponsToCharacterID int,  
@CharacterID int,
@WeaponID int
AS
BEGIN
	UPDATE WeaponsToCharacters SET
	CharacterID = @CharacterID,
	WeaponID = @WeaponID
		WHERE WeaponsToCharacterID = @WeaponsToCharacterID
END
GO


CREATE PROCEDURE sproc_WeaponsToCharacterDelete
@WeaponsToCharacterID int  
AS
BEGIN
  DELETE FROM WeaponsToCharacters
  WHERE WeaponsToCharacterID = @WeaponsToCharacterID;
END
GO


CREATE PROCEDURE sproc_WeaponCategoryAdd
@WeaponCategoryID int OUTPUT,
@Name nvarchar(45)
AS
BEGIN
  INSERT INTO WeaponCategories (Name) 
  VALUES (@Name);
  SET @WeaponCategoryID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_WeaponCategoryUpdate
@WeaponCategoryID int,
@Name nvarchar(45)
AS
BEGIN
	UPDATE WeaponCategories SET
	Name = @Name
		WHERE WeaponCategoryID = @WeaponCategoryID
END
GO


CREATE PROCEDURE sproc_WeaponCategoryDelete
@WeaponCategoryID int  
AS
BEGIN
  DELETE FROM WeaponCategories
  WHERE WeaponCategoryID = @WeaponCategoryID;
END
GO


CREATE PROCEDURE sproc_WeaponTypeAdd
@WeaponTypeID int OUTPUT,
@Name nvarchar(45),
@AttackDiceSmall nvarchar(45),
@AttackDiceMedium nvarchar(45),
@AttackRange int, 
@Critical nvarchar(45),
@GPValue int,
@Weight int,
@WeaponCategoryID int,
@WeaponCoreTypeID int
AS
BEGIN
  INSERT INTO WeaponTypes (Name, AttackDiceSmall, AttackDiceMedium, AttackRange, Critical, GPValue, Weight, WeaponCategoryID, WeaponCoreTypeID) 
  VALUES (@Name,  @AttackDiceSmall, @AttackDiceMedium, @AttackRange, @Critical, @GPValue, @Weight, @WeaponCategoryID, @WeaponCoreTypeID);
  SET @WeaponTypeID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_WeaponTypeUpdate
@WeaponTypeID int,
@Name nvarchar(45),
@AttackDiceSmall nvarchar(45),
@AttackDiceMedium nvarchar(45),
@AttackRange int, 
@Critical nvarchar(45),
@GPValue int,
@Weight int,
@WeaponCategoryID int,
@WeaponCoreTypeID int
AS
BEGIN
	UPDATE WeaponTypes SET
	Name = @Name,
	AttackDiceSmall = @AttackDiceSmall,
	AttackDiceMedium = @AttackDiceMedium,
	AttackRange = @AttackRange,
	Critical = @Critical,
	GPValue = @GPValue,
	Weight = @Weight,
	WeaponCategoryID = @WeaponCategoryID,
	WeaponCoreTypeID = @WeaponCoreTypeID
		WHERE WeaponTypeID = @WeaponTypeID
END
GO


CREATE PROCEDURE sproc_WeaponTypeDelete
@WeaponTypeID int  
AS
BEGIN
  DELETE FROM WeaponTypes
  WHERE WeaponTypeID = @WeaponTypeID;
END
GO


CREATE PROCEDURE sprocAlignmentGet
@AlignmentID int  
AS
BEGIN
  SELECT * FROM Alignments
  WHERE AlignmentID = @AlignmentID;
END
GO


CREATE PROCEDURE sprocAlignmentsGetAll
AS
BEGIN
  SELECT * FROM Alignments;
END
GO


CREATE PROCEDURE sprocArmorAddOnGet
@ArmorAddOnID int  
AS
BEGIN
  SELECT * FROM ArmorAddOns
  WHERE ArmorAddOnID = @ArmorAddOnID;
END
GO


CREATE PROCEDURE sprocArmorAddOnsGetAll
AS
BEGIN
  SELECT * FROM ArmorAddOns;
END
GO


CREATE PROCEDURE sprocArmorGet
@ArmorID int  
AS
BEGIN
  SELECT * FROM Armors
  WHERE ArmorID = @ArmorID;
END
GO


CREATE PROCEDURE sprocArmorsGetAll  
AS
BEGIN
  SELECT * FROM Armors;
END
GO


CREATE PROCEDURE sprocArmorsGetForCharacter
@CharacterID int  
AS
BEGIN
  SELECT * FROM ArmorsToCharacters ac
  JOIN Armors a ON a.ArmorID = ac.ArmorID
  WHERE CharacterID = @CharacterID;
END
GO


CREATE PROCEDURE sprocArmorToCharacterGet
@ArmorToCharacterID int  
AS
BEGIN
  SELECT * FROM ArmorsToCharacters
  WHERE ArmorsToCharacterID = @ArmorToCharacterID;
END
GO

CREATE PROCEDURE sprocArmorCoreTypeGet
@ArmorCoreTypeID int  
AS
BEGIN
  SELECT * FROM ArmorCoreTypes
  WHERE ArmorCoreTypeID = @ArmorCoreTypeID;
END
GO


CREATE PROCEDURE sprocArmorCoreTypesGetAll  
AS
BEGIN
  SELECT * FROM ArmorCoreTypes;
END
GO

CREATE PROCEDURE sprocArmorTypeGet
@ArmorTypeID int  
AS
BEGIN
  SELECT * FROM ArmorTypes
  WHERE ArmorTypeID = @ArmorTypeID;
END
GO


CREATE PROCEDURE sprocArmorTypesGetAll  
AS
BEGIN
  SELECT * FROM ArmorTypes;
END
GO

CREATE PROCEDURE sprocBloodlineGet
@BloodlineID int  
AS
BEGIN
  SELECT * FROM Bloodlines
  WHERE BloodlineID = @BloodlineID;
END
GO


CREATE PROCEDURE sprocBloodlinesGetAll  
AS
BEGIN
  SELECT * FROM Bloodlines;
END
GO


CREATE PROCEDURE sprocCampaignGet
@CampaignID int  
AS
BEGIN
  SELECT * FROM Campaigns
  WHERE CampaignID = @CampaignID;
END
GO


CREATE PROCEDURE sprocCampaignsGetAll
AS
BEGIN
  SELECT * FROM Campaigns;
END
GO


CREATE PROCEDURE sprocCharacterGet
@CharacterID int  
AS
BEGIN
  SELECT * FROM Characters
  WHERE CharacterID = @CharacterID;
END
GO


CREATE PROCEDURE sprocCharactersGetForCampaign
@CampaignID int 
AS
BEGIN
  SELECT * FROM Characters
  WHERE CampaignID = @CampaignID;
END
GO


CREATE PROCEDURE sprocCharacterClassesGetAll
AS
BEGIN
  SELECT * FROM CharacterClasses;
END
GO


CREATE PROCEDURE sprocClassesGetForCharacter
@CharacterID int  
AS
BEGIN
  SELECT * FROM ClassesToCharacters cc
  JOIN CharacterClasses c ON c.CharacterClassID = cc.CharacterClassID
  WHERE CharacterID = @CharacterID;
END
GO


CREATE PROCEDURE sprocClassesToCharacterGet
@ClassesToCharacterID int  
AS
BEGIN
  SELECT * FROM ClassesToCharacters
  WHERE ClassesToCharacterID = @ClassesToCharacterID;
END
GO


CREATE PROCEDURE sprocCharacterClassGet
@CharacterClassID int  
AS
BEGIN
  SELECT * FROM CharacterClasses
  WHERE CharacterClassID = @CharacterClassID;
END
GO


CREATE PROCEDURE sprocConditionGet
@ConditionID int  
AS
BEGIN
  SELECT * FROM Conditions
  WHERE ConditionID = @ConditionID;
END
GO


CREATE PROCEDURE sprocConditionsGetAll
AS
BEGIN
  SELECT * FROM Conditions;
END
GO


CREATE PROCEDURE sprocWeaponCoreTypeGet
@WeaponCoreTypeID int  
AS
BEGIN
  SELECT * FROM WeaponCoreTypes
  WHERE WeaponCoreTypeID = @WeaponCoreTypeID;
END
GO


CREATE PROCEDURE sprocWeaponCoreTypesGetAll  
AS
BEGIN
  SELECT * FROM WeaponCoreTypes;
END
GO


CREATE PROCEDURE sprocDamageTypeGet
@DamageTypeID int  
AS
BEGIN
  SELECT * FROM DamageTypes
  WHERE DamageTypeID = @DamageTypeID;
END
GO


CREATE PROCEDURE sprocDamageTypesGetAll
AS
BEGIN
  SELECT * FROM DamageTypes;
END
GO


CREATE PROCEDURE sprocDeitiesGetAll
AS
BEGIN
  SELECT * FROM Deities;
END
GO


CREATE PROCEDURE sprocDeityGet
@DeityID int  
AS
BEGIN
  SELECT * FROM Deities
  WHERE DeityID = @DeityID;
END
GO


CREATE PROCEDURE sprocDomainGet
@DomainID int  
AS
BEGIN
  SELECT * FROM [Domains]
  WHERE DomainID = @DomainID;
END
GO


CREATE PROCEDURE sprocDomainsGetAll  
AS
BEGIN
  SELECT * FROM [Domains];
END
GO


CREATE PROCEDURE sprocFeatTypeGet
@FeatTypeID int  
AS
BEGIN
  SELECT * FROM FeatTypes
  WHERE FeatTypeID = @FeatTypeID;
END
GO


CREATE PROCEDURE sprocFeatTypesGetAll  
AS
BEGIN
  SELECT * FROM FeatTypes;
END
GO


CREATE PROCEDURE sprocFeatGet
@FeatID int  
AS
BEGIN
  SELECT * FROM Feats
  WHERE FeatID = @FeatID;
END
GO


CREATE PROCEDURE sprocFeatsGetAll  
AS
BEGIN
  SELECT * FROM Feats;
END
GO

CREATE PROCEDURE sprocItemGet
@ItemID int  
AS
BEGIN
  SELECT * FROM Items
  WHERE ItemID = @ItemID;
END
GO


CREATE PROCEDURE sprocItemsGetAll
AS
BEGIN
  SELECT * FROM Items;
END
GO


CREATE PROCEDURE sprocMagicSchoolGet
@MagicSchoolID int  
AS
BEGIN
  SELECT * FROM MagicSchools
  WHERE MagicSchoolID = @MagicSchoolID;
END
GO


CREATE PROCEDURE sprocMagicSchoolsGetAll  
AS
BEGIN
  SELECT * FROM MagicSchools;
END
GO


CREATE PROCEDURE sprocMaterialGet
@MaterialID int  
AS
BEGIN
  SELECT * FROM Materials
  WHERE MaterialID = @MaterialID;
END
GO


CREATE PROCEDURE sprocMaterialsGetAll  
AS
BEGIN
  SELECT * FROM Materials;
END
GO


CREATE PROCEDURE sprocPlayerGet
@PlayerID int  
AS
BEGIN
  SELECT * FROM Players
  WHERE PlayerID = @PlayerID;
END
GO


CREATE PROCEDURE sprocRaceGet
@RaceID int  
AS
BEGIN
  SELECT * FROM Races
  WHERE RaceID = @RaceID;
END
GO


CREATE PROCEDURE sprocRacesGetAll
AS
BEGIN
  SELECT * FROM Races;
END
GO


CREATE PROCEDURE sprocSpellGet
@SpellID int  
AS
BEGIN
  SELECT * FROM Spells
  WHERE SpellID = @SpellID;
END
GO


CREATE PROCEDURE sprocSpellsGetAll  
AS
BEGIN
  SELECT * FROM Spells;
END
GO


CREATE PROCEDURE sprocSpellsGetForCharacter
@CharacterID int  
AS
BEGIN
  SELECT * FROM SpellsToCharacters sc
  JOIN Spells s ON s.SpellID = sc.SpellID
  WHERE CharacterID = @CharacterID;
END
GO


CREATE PROCEDURE sprocSpellsToCharacterGet
@SpellsToCharacterID int  
AS
BEGIN
  SELECT * FROM SpellsToCharacters
  WHERE SpellsToCharacterID = @SpellsToCharacterID;
END
GO


CREATE PROCEDURE sprocSlotGet
@SlotID int  
AS
BEGIN
  SELECT * FROM Slots
  WHERE SlotID = @SlotID;
END
GO


CREATE PROCEDURE sprocSlotsGetAll
AS
BEGIN
  SELECT * FROM Slots;
END
GO


CREATE PROCEDURE sprocWeaponGet
@WeaponID int  
AS
BEGIN
  SELECT * FROM Weapons
  WHERE WeaponID = @WeaponID;
END
GO


CREATE PROCEDURE sprocWeaponsGetAll
AS
BEGIN
  SELECT * FROM Weapons;
END
GO


CREATE PROCEDURE sprocWeaponsGetForCharacter
@CharacterID int  
AS
BEGIN
  SELECT * FROM WeaponsToCharacters wc
  JOIN Weapons w ON w.WeaponID = wc.WeaponID
  WHERE CharacterID = @CharacterID;
END
GO


CREATE PROCEDURE sprocWeaponsToCharacterGet
@WeaponsToCharacterID int  
AS
BEGIN
  SELECT * FROM WeaponsToCharacters
  WHERE WeaponsToCharacterID = @WeaponsToCharacterID;
END
GO


CREATE PROCEDURE sprocWeaponCategoryGet
@WeaponCategoryID int  
AS
BEGIN
  SELECT * FROM WeaponCategories
  WHERE WeaponCategoryID = @WeaponCategoryID;
END
GO


CREATE PROCEDURE sprocWeaponCategoriesGetAll 
AS
BEGIN
  SELECT * FROM WeaponCategories;
END
GO


CREATE PROCEDURE sprocWeaponTypeGet
@WeaponTypeID int  
AS
BEGIN
  SELECT * FROM WeaponTypes
  WHERE WeaponTypeID = @WeaponTypeID;
END
GO


CREATE PROCEDURE sprocWeaponTypesGetAll  
AS
BEGIN
  SELECT * FROM WeaponTypes;
END
GO