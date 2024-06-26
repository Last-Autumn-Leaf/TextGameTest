﻿/*
------------------------------------------------
Generated by Cradle 2.0.1.0
https://github.com/daterre/Cradle

Original file: SarahTest.html
Story format: Harlowe
------------------------------------------------
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradle;
using IStoryThread = System.Collections.Generic.IEnumerable<Cradle.StoryOutput>;
using Cradle.StoryFormats.Harlowe;

public partial class @SarahTest: Cradle.StoryFormats.Harlowe.HarloweStory
{
	#region Variables
	// ---------------

	public class VarDefs: RuntimeVars
	{
		public VarDefs()
		{
			VarDef("player", () => this.@player, val => this.@player = val);
			VarDef("name", () => this.@name, val => this.@name = val);
		}

		public StoryVar @player;
		public StoryVar @name;
	}

	public new VarDefs Vars
	{
		get { return (VarDefs) base.Vars; }
	}

	// ---------------
	#endregion

	#region Initialization
	// ---------------

	public readonly Cradle.StoryFormats.Harlowe.HarloweRuntimeMacros macros1;

	@SarahTest()
	{
		this.StartPassage = "start";

		base.Vars = new VarDefs() { Story = this, StrictMode = true };

		macros1 = new Cradle.StoryFormats.Harlowe.HarloweRuntimeMacros() { Story = this };

		base.Init();
		passage1_Init();
		passage2_Init();
		passage3_Init();
	}

	// ---------------
	#endregion

	// .............
	// #1: start

	void passage1_Init()
	{
		this.Passages[@"start"] = new StoryPassage(@"start", new string[]{ "START", }, passage1_Main);
	}

	IStoryThread passage1_Main()
	{
		Vars.player  = false;
		Vars.name  = "Sarah";
		yield return lineBreak();
		yield return text("Hello Carl");
		yield return lineBreak();
		yield return text("Are you free today ?");
		yield return lineBreak();
		Vars.player  = true;
		yield return lineBreak();
		yield return link("Yes", "agree", null);
		yield return lineBreak();
		yield return link("No", "disagree", null);
		yield break;
	}


	// .............
	// #2: agree

	void passage2_Init()
	{
		this.Passages[@"agree"] = new StoryPassage(@"agree", new string[]{ "PLAYER", }, passage2_Main);
	}

	IStoryThread passage2_Main()
	{
		yield return text("Sure, what you tryna do ?");
		Vars.player  = false;
		yield return lineBreak();
		yield return text("Can i call you ? I really need someone to talk to right now...");
		Vars.player  = true;
		yield return lineBreak();
		yield return text("Sure wait a sec.");
		yield break;
	}


	// .............
	// #3: disagree

	void passage3_Init()
	{
		this.Passages[@"disagree"] = new StoryPassage(@"disagree", new string[]{ "PLAYER", }, passage3_Main);
	}

	IStoryThread passage3_Main()
	{
		yield return text("Nah already got plans.");
		yield return lineBreak();
		yield return text("Sorry.");
		Vars.player  = false;
		yield return lineBreak();
		yield return text("Ho okay");
		yield return lineBreak();
		yield return text("nvm have a good day");
		yield break;
	}


}