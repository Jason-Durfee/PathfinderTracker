DROP DATABASE PathfinderTracker
CREATE DATABASE PathfinderTracker
USE PathfinderTracker
GO

CREATE TABLE Alignments(
	AlignmentID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	Abbreviation nvarchar(10) NOT NULL,
	Description nvarchar(255) NOT NULL,
)

CREATE TABLE ArmorTypes(
	ArmorTypeID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
)

CREATE TABLE Campaigns(
	CampaignID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
)

CREATE TABLE Classes(
	ClassID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	SubClassID int NOT NULL,
)

CREATE TABLE DamageTypes(
	DamageTypeID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
)

CREATE TABLE Deities(
	DeityID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	Description nvarchar(255) NOT NULL,
	AlignmentID int NOT NULL,
)

CREATE TABLE Feats(
	FeatID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	Description nvarchar(45) NOT NULL,
)

CREATE TABLE MagicSchools(
	MagicSchoolID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	Description nvarchar(45) NOT NULL,
)

CREATE TABLE Materials(
	MaterialID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
)

CREATE TABLE Races(
	RaceID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
)

CREATE TABLE Characters(
	CharacterID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	Level int NOT NULL,
	IsNPC bit NOT NULL,
	AlignmentID int NOT NULL,
	CONSTRAINT AlignmentToCharacter FOREIGN KEY(AlignmentID)
	REFERENCES Alignments (AlignmentID),
	DeityID int NOT NULL,
	CONSTRAINT DeityToCharacter FOREIGN KEY(DeityID)
	REFERENCES Deities (DeityID),
	RaceID int NOT NULL,
	CONSTRAINT RaceToCharacter FOREIGN KEY(RaceID)
	REFERENCES Races (RaceID),
	CampaignID int NOT NULL,
	CONSTRAINT CampaignToCharacter FOREIGN KEY(CampaignID)
	REFERENCES Campaigns (CampaignID)
)

CREATE TABLE Players(
	PlayerID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	HPMax int NOT NULL,
	HPCurrent int NOT NULL,
	CharacterID int NOT NULL,
	CONSTRAINT CharacterToPlayer FOREIGN KEY(CharacterID)
	REFERENCES Characters (CharacterID),
	Bonuses nvarchar(255) NOT NULL,
)

CREATE TABLE ClassesToCharacters(
	ClassesToCharacterID int IDENTITY(1,1) PRIMARY KEY,
	ClassLevel int NOT NULL,
	CharacterID int NOT NULL,
	CONSTRAINT CharacterToClass FOREIGN KEY(CharacterID)
	REFERENCES Characters (CharacterID),
	ClassID int NOT NULL,
	CONSTRAINT ClassToCharacter FOREIGN KEY(ClassID)
	REFERENCES Classes (ClassID)
)

CREATE TABLE Spells(
	SpellID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	Description nvarchar(255) NOT NULL,
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

CREATE TABLE SubClasses(
	SubClassID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	Description nvarchar(255) NOT NULL,
)

CREATE TABLE WeaponSubTypes(
	WeaponSubTypeID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
)

CREATE TABLE WeaponTypes(
	WeaponTypeID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
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
	Name nvarchar(45) NOT NULL,
	AttackDiceSmall nvarchar(45) NOT NULL,
	AttackDiceMedium nvarchar(45) NOT NULL,
	Critical nvarchar(45) NOT NULL,
	SpecialAttributes nvarchar(255) NOT NULL,
	GPValue int NOT NULL,
	AttackRange int NOT NULL,
	Weight int NOT NULL,
	WeaponTypeID int NOT NULL,
	CONSTRAINT WeaponTypeToWeapon FOREIGN KEY(WeaponTypeID)
	REFERENCES WeaponTypes (WeaponTypeID),
	DamageTypeID int NOT NULL,
	CONSTRAINT DamageTypeToWeapon FOREIGN KEY(DamageTypeID)
	REFERENCES DamageTypes (DamageTypeID),
	MaterialID int NOT NULL,
	CONSTRAINT MaterialToWeapon FOREIGN KEY(MaterialID)
	REFERENCES Materials (MaterialID)
)

CREATE TABLE Armors(
	ArmorID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(45) NOT NULL,
	SpecialAttributes nvarchar(255) NOT NULL,
	GPValue int NOT NULL,
	ACBonus int NOT NULL,
	ArmorCheckPenalty int NOT NULL,
	ArcaneSpellFailureChance int NOT NULL,
	Weight int NOT NULL,
	ArmorTypeID int NOT NULL,
	CONSTRAINT ArmorTypeToArmor FOREIGN KEY(ArmorTypeID)
	REFERENCES ArmorTypes (ArmorTypeID),
	ArmorAddonID int NOT NULL,
	CONSTRAINT ArmorAddonToArmor FOREIGN KEY(ArmorAddonID)
	REFERENCES ArmorAddons (ArmorAddonID),
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


CREATE PROCEDURE sproc_AlignmentAdd
@AlignmentID int OUTPUT, 
@Name nvarchar(45),
@Abbreviation nvarchar(10),
@Description nvarchar(255)
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
@Description nvarchar(255)
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
@Name nvarchar(45),
@SpecialAttributes nvarchar(255), 
@ACBonus int,
@GPValue int,
@Weight int,
@ArmorTypeID int,
@ArmorAddonID int, 
@ArmorCheckPenalty int,
@ArcaneSpellFailureChance int,
@MaterialID int
AS
BEGIN
  INSERT INTO Armors (Name, SpecialAttributes, ACBonus, GPValue, Weight,
	ArmorCheckPenalty, ArcaneSpellFailureChance, MaterialID, ArmorTypeID, ArmorAddonID) 
  VALUES (@Name, @SpecialAttributes, @ACBonus, @GPValue, @Weight,
	@ArmorCheckPenalty, @ArcaneSpellFailureChance, @MaterialID, @ArmorTypeID, @ArmorAddonID);
  SET @ArmorID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_ArmorUpdate
@ArmorID int, 
@Name nvarchar(45),
@SpecialAttributes nvarchar(255), 
@ACBonus int,
@GPValue int,
@Weight int,
@ArmorTypeID int,
@ArmorAddonID int, 
@ArmorCheckPenalty int,
@ArcaneSpellFailureChance int,
@MaterialID int
AS
BEGIN
	UPDATE Armors	SET 
	Name = @Name,
	SpecialAttributes = @SpecialAttributes,
	ACBonus = @ACBonus,
	GPValue = @GPValue,
	Weight = @Weight,
	ArmorTypeID = @ArmorTypeID,
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
@RaceID int,
@CampaignID int
AS
BEGIN
  INSERT INTO Characters (Name, Level, IsNPC, AlignmentID, DeityID, RaceID, CampaignID) 
  VALUES (@Name, @Level, @IsNPC, @AlignmentID, @DeityID, @RaceID, @CampaignID);
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
@RaceID int,
@CampaignID int
AS
BEGIN
	UPDATE Characters SET
	Name = @Name,
	Level = @Level,
	IsNPC = @IsNPC,
	AlignmentID = @AlignmentID,
	DeityID = @DeityID,
	RaceID = @RaceID,
	CampaignID = @CampaignID
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


CREATE PROCEDURE sproc_ClassAdd
@ClassID int OUTPUT, 
@Name nvarchar(45),
@SubClassID int
AS
BEGIN
  INSERT INTO Classes (Name, SubClassID) 
  VALUES (@Name, @SubClassID);
  SET @ClassID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_ClassUpdate
@ClassID int, 
@Name nvarchar(45),
@SubClassID int
AS
BEGIN
	UPDATE Classes SET
	Name = @Name,
	SubClassID = @SubClassID
		WHERE ClassID = @ClassID
END
GO


CREATE PROCEDURE sproc_ClassDelete
@ClassID int  
AS
BEGIN
  DELETE FROM Classes
  WHERE ClassID = @ClassID;
END
GO

CREATE PROCEDURE sproc_ClassesToCharacterAdd
@ClassesToCharacterID int OUTPUT, 
@ClassLevel int,
@CharacterID int,
@ClassID int
AS
BEGIN
  INSERT INTO ClassesToCharacters (ClassLevel, CharacterID, ClassID) 
  VALUES (@ClassLevel, @CharacterID, @ClassID);
  SET @ClassesToCharacterID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_ClassesToCharacterUpdate
@ClassesToCharacterID int, 
@ClassLevel int,
@CharacterID int,
@ClassID int
AS
BEGIN
	UPDATE ClassesToCharacters SET
	ClassLevel = @ClassLevel,
	CharacterID = @CharacterID,
	ClassID = @ClassID
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


CREATE PROCEDURE sproc_DeityAdd
@DeityID int OUTPUT, 
@Name nvarchar(45),
@AlignmentID int,
@Description nvarchar(255)
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
@Description nvarchar(255)
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


CREATE PROCEDURE sproc_FeatAdd
@FeatID int OUTPUT,
@Name nvarchar(45),
@Description nvarchar(255)
AS
BEGIN
  INSERT INTO Feats (Name, Description) 
  VALUES (@Name, @Description);
  SET @FeatID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_FeatUpdate
@FeatID int,
@Name nvarchar(45),
@Description nvarchar(255)
AS
BEGIN
	UPDATE Feats SET
	Name = @Name,
	Description = @Description
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


CREATE PROCEDURE sproc_MagicSchoolAdd
@MagicSchoolID int OUTPUT,
@Name nvarchar(45),
@Description nvarchar(255)
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
@Description nvarchar(255)
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
@Name nvarchar(45)
AS
BEGIN
  INSERT INTO Materials (Name) 
  VALUES (@Name);
  SET @MaterialID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_MaterialUpdate
@MaterialID int,
@Name nvarchar(45)
AS
BEGIN
	UPDATE Materials SET
	Name = @Name
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
@Name nvarchar(45),
@HPMax int,
@HPCurrent int,
@CharacterID int, 
@Bonuses nvarchar(255)
AS
BEGIN
  INSERT INTO Players (Name, HPMax, HPCurrent, CharacterID, Bonuses) 
  VALUES (@Name, @HPMax, @HPCurrent, @CharacterID, @Bonuses);
  SET @PlayerID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_PlayerUpdate
@PlayerID int,
@Name nvarchar(45),
@HPMax int,
@HPCurrent int,
@CharacterID int, 
@Bonuses nvarchar(255)
AS
BEGIN
	UPDATE Players SET
	Name = @Name,
	HPMax = @HPMax,
	HPCurrent = @HPCurrent,
	CharacterID = @CharacterID,
	Bonuses = @Bonuses
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
@Description nvarchar(255),
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
@Description nvarchar(255),
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


CREATE PROCEDURE sproc_SubClassAdd
@SubClassID int OUTPUT,  
@Name nvarchar(45),
@Description nvarchar(255)
AS
BEGIN
  INSERT INTO SubClasses (Name, Description) 
  VALUES (@Name, @Description);
  SET @SubClassID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_SubClassUpdate
@SubClassID int,  
@Name nvarchar(45),
@Description nvarchar(255)
AS
BEGIN
	UPDATE SubClasses SET
	Name = @Name,
	Description = @Description
		WHERE SubClassID = @SubClassID
END
GO


CREATE PROCEDURE sproc_SubClassDelete
@SubClassID int  
AS
BEGIN
  DELETE FROM SubClasses
  WHERE SubClassID = @SubClassID;
END
GO


CREATE PROCEDURE sproc_WeaponAdd
@WeaponID int OUTPUT,
@Name nvarchar(45),
@AttackDiceSmall nvarchar(45),
@AttackDiceMedium nvarchar(45),
@AttackRange int, 
@Critical nvarchar(45),
@SpecialAttributes nvarchar(255),
@GPValue int,
@Weight int, 
@WeaponTypeID int,
@DamageTypeID int,
@MaterialID int
AS
BEGIN
  INSERT INTO Weapons (Name, AttackDiceSmall, AttackDiceMedium, AttackRange, Critical, SpecialAttributes, GPValue, Weight,
	WeaponTypeID, DamageTypeID, MaterialID) 
  VALUES (@Name, @AttackDiceSmall, @AttackDiceMedium, @AttackRange, @Critical, @SpecialAttributes, @GPValue, @Weight,
	@WeaponTypeID, @DamageTypeID, @MaterialID);
  SET @WeaponID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_WeaponUpdate
@WeaponID int,
@Name nvarchar(45),
@AttackDiceSmall nvarchar(45),
@AttackDiceMedium nvarchar(45),
@AttackRange int, 
@Critical nvarchar(45),
@SpecialAttributes nvarchar(255),
@GPValue int,
@Weight int, 
@WeaponTypeID int,
@DamageTypeID int,
@MaterialID int
AS
BEGIN
	UPDATE Weapons SET
	Name = @Name,
	AttackDiceSmall = @AttackDiceSmall,
	AttackDiceMedium = @AttackDiceMedium,
	AttackRange = @AttackRange,
	Critical = @Critical,
	SpecialAttributes = @SpecialAttributes,
	GPValue = @GPValue,
	Weight = @Weight,
	WeaponTypeID = @WeaponTypeID,
	DamageTypeID = @DamageTypeID,
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


CREATE PROCEDURE sproc_WeaponSubTypeAdd
@WeaponSubTypeID int OUTPUT,
@Name nvarchar(45)
AS
BEGIN
  INSERT INTO WeaponSubTypes (Name) 
  VALUES (@Name);
  SET @WeaponSubTypeID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_WeaponSubTypeUpdate
@WeaponSubTypeID int,
@Name nvarchar(45)
AS
BEGIN
	UPDATE WeaponSubTypes SET
	Name = @Name
		WHERE WeaponSubTypeID = @WeaponSubTypeID
END
GO


CREATE PROCEDURE sproc_WeaponSubTypeDelete
@WeaponSubTypeID int  
AS
BEGIN
  DELETE FROM WeaponSubTypes
  WHERE WeaponSubTypeID = @WeaponSubTypeID;
END
GO


CREATE PROCEDURE sproc_WeaponTypeAdd
@WeaponTypeID int OUTPUT,
@Name nvarchar(45)
AS
BEGIN
  INSERT INTO WeaponTypes (Name) 
  VALUES (@Name);
  SET @WeaponTypeID = @@IDENTITY;
END
GO


CREATE PROCEDURE sproc_WeaponTypeUpdate
@WeaponTypeID int,
@Name nvarchar(45)
AS
BEGIN
	UPDATE WeaponTypes SET
	Name = @Name
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


CREATE PROCEDURE sprocClassesGetAll
AS
BEGIN
  SELECT * FROM Classes;
END
GO


CREATE PROCEDURE sprocClassesGetForCharacter
@CharacterID int  
AS
BEGIN
  SELECT * FROM ClassesToCharacters cc
  JOIN Classes c ON c.ClassID = cc.ClassID
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


CREATE PROCEDURE sprocClassGet
@ClassID int  
AS
BEGIN
  SELECT * FROM Classes
  WHERE ClassID = @ClassID;
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


CREATE PROCEDURE sprocPlayersGetForCampaign
@CampaignID int  
AS
BEGIN
  SELECT * FROM Players p
  JOIN Characters c ON p.CharacterID = c.CharacterID
  WHERE c.CampaignID = @CampaignID;
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


CREATE PROCEDURE sprocSubClassesGetAll
AS
BEGIN
  SELECT * FROM SubClasses;
END
GO


CREATE PROCEDURE sprocSubClassGet
@SubClassID int  
AS
BEGIN
  SELECT * FROM SubClasses
  WHERE SubClassID = @SubClassID;
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


CREATE PROCEDURE sprocWeaponSubTypeGet
@WeaponSubTypeID int  
AS
BEGIN
  SELECT * FROM WeaponSubTypes
  WHERE WeaponSubTypeID = @WeaponSubTypeID;
END
GO


CREATE PROCEDURE sprocWeaponSubTypesGetAll 
AS
BEGIN
  SELECT * FROM WeaponSubTypes;
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