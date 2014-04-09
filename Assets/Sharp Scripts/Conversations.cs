using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;



public class Conversations : MonoBehaviour {

	public Texture texture;
	public Texture timer;
	private string message;
	private int currentSelection;
	private int currentConversation;
	private GameObject player;
	private float lastTime;
	private float currentTime;
	private float timeBetweenChoices;

	GUIStyle selectionGuiStyle = new GUIStyle();
	GUIStyle answerGuiStyle = new GUIStyle();
	
	void OnGUI () {
		Rect rConsoleGraphic, rAnswer;
		int x,y,w,h;

		selectionGuiStyle.fontSize = 30;
		selectionGuiStyle.fontStyle = FontStyle.Bold;

		answerGuiStyle.fontSize = 20;

		//Draw Question Box
		x = (int)(Screen.width * 0.65);
		y = 20;
		w = Screen.width - x;
		h = 800;

		rConsoleGraphic = new Rect(x, y, w, h);
		GUI.DrawTexture(rConsoleGraphic, texture, ScaleMode.StretchToFill, true, 10.0F);

		//Draw Answer Boxes
		w = w - (x * 1/10);
		h = 160;
		x = x + (x * 1/20);
		y = 80;

		for (int i = 0; i < 4; i++){
			message = a[currentConversation, i];
			rAnswer = new Rect (x, y + (i * h), w, h);
			
			if (currentSelection == i) {
				GUI.color = Color.red;
				GUI.skin.textArea.fontSize = 30;
				GUI.skin.textArea.fontStyle = FontStyle.Bold;
				GUI.TextArea(rAnswer, message);
			} else {
				GUI.color = Color.green;
				GUI.skin.textArea.fontSize = 20;
				GUI.TextArea(rAnswer, message);
			}
		}
		GUI.color = Color.blue;
		GUI.DrawTexture(new Rect(3*Screen.width/4, 30, 8*(timeBetweenChoices - (Time.time-lastTime)),32), timer, ScaleMode.StretchToFill, true, 10.0F);
		

	}

	void Start(){
		player = GameObject.Find("Player");
		lastTime = Time.time;
		currentTime = Time.time;
		currentConversation = 0;
		timeBetweenChoices = 8.0f;
	}

	void Update(){
		if(player == null){
			player = GameObject.Find("Player");
		}
		//move response based on Player Height
		currentSelection = 2;
		if (player.transform.position.y < 1.5){
			currentSelection = 3;
		} else if (player.transform.position.y > 2.6){
			currentSelection = 1;
		} else {
			currentSelection = 2;
		}

		//Select response based on Timer
		currentTime = Time.time;
		if ((currentTime - lastTime) >= timeBetweenChoices){
			if (currentSelection == 1) {
				currentConversation = b[currentConversation, 0];
			} else if (currentSelection == 2) {
				currentConversation = b[currentConversation, 1];
			} else {
				currentConversation = b[currentConversation, 2];
			}

			if (currentConversation == -1 || currentConversation > a.GetLength(0) - 1) {
				currentConversation = UnityEngine.Random.Range(0, a.GetLength(0) - 1);
			}
			lastTime = lastTime + timeBetweenChoices;
		}
	}

	#region Dragons Be Here

	/*       x = id
	 * a[x, 0] = question1
	 * a[x, 1] = answer1
	 * a[x, 2] = answer2
	 * a[x, 3] = answer3
	 */

	string[,] a = new string[,]
	{
		{"You enter a strange room. Hibiscus-flower wallpaper covers the walls and a single lamp shines in the center. What do you do?", "I pray to Mithra for guidance", "I solve the riddle, because I'm psychic. ", "Who are you and why are you talking to me?"},
		{"Praying to Mithra, a bold move. Roll a d20 to see what happens. ","Natural 20, what now nerd?","(Palm the dice) Mithra be praised! A Natural 20, what noweth nerd?","This is some weird foreplay. Keep going, I like it. "},
		{"The ghost of David Beckham, the world's greatest ghost magician, enters the room through a hidden door. In a high pitched voice, the ghost of David Beckham congratulates you. What do you do?","(Action) Run, ghosts are no joke. ","Thank the ghost of David Beckham with your mind voice, because you're psychic.","This is some weird foreplay. Keep going, I like it. "},
		{"Thank you for applying to join our company. Why do you think you would be a good fit here?", "I'm a hard worker, smart too!", "I want money, you have money.", "I can count to 10 in 3 languages. My mother is a proud mother. "},
		{"Wow! Doesn't that give you a special item?", "(Lie) I think Mithra gives me some extra gold this turn.", "(Lie) Yeah, I get a freaking MAGIC SWORD!", "(Mega lie) Just give me a machine gun, nerd. "}, 
		{"Screaming and shrieking, you skitter and crawl. Srugling to stand, as your cellphone calls. ", "ANSWER THE PHONE. ", "IGNORE THE PHONE. ", "EAT THE PHONE. "}, 
		{"Welcome to Bacon Muffin! Would you like bacon on your bacon muffin?", "Yes.", "No.", "Much bacon."}, 
		{"How about some guacamole! Its guca-GREAT!", "Yes.", "No.", "Much guacamole. "}, 
		{"Thanks for choosing Bacon Muffin's muffins and bacon! That will be $12.50	", "(Give the money, leave in shame) Thanks. ", "Nothing tastes better than diabetes feels. ", "Can I have that to go? "},
		{"You sit down to eat your Bacon Muffin brand bacon muffin is silent shame. Alone. ", "(Eat the muffin slowly.) ", "(Cry into a paper bag.)", "(Order another muffin, you ate your first muffin before you sat down.)"},
		{"A bus arrives at your stop. You didn't notice the number. What do you do? ", "Ask the driver where the bus is headed. ", "Try to talk to the driver, but forget how to English instead.", "Moon the bus. "}, 
		{"The bus driver lifts her head and says, in a scratchy voice, \"Henderson\". ", "Nope. (Run.)", "Nope nope nope. (Run faster.)	", "Get on the bus (Are you even reading this?)"}, 
		{"Undead Hendersonians poor out of the bus. Teeth gnashing, claws flying, they seek your blood. A blue jeaned, black t-shirted horde of death. What do you do? ", "*Meet your fate.* ", "*Channel your inner Brad Pitt.*", "*Dive into a nearby psychiatrist's office.* "},
		{"I see, we are looking for a person who has 3 years' experience. How many years of experience do you have? ", "Between enough and a lot. ", "..... Three and a half? Maybe.", "Purple! "}, 
		{"Your hired! Welcome aboard. You start Monday!", "Really?",  "That was easy. ", "\"You're\""},
		{"No, you swine. Get out of my office. Never call me again.", "But...",  "What will I tell my cats? 	", "No YOU never call ME again! "}, 
		{"So am I. ","I'm gonna leave. ", "Oh my. ", "Everything is coming up Milhouse. "},
		{"Hey Dude, you're awesome!","No, you're awesome!", "Glad we're in agreement. ", "Please stop contacting me Todd. "},
		{"Dude, you are AWESOME. ","NA BRO, YOU'RE AWESOME. ", "Can we stop, we do this like 50 times a day. ", "Turbo bro-fist yolo! "},
		{"I'm just glad to be in anything.","That's what your mum said. ", "I must go, my people need me. *flight boots*", "Thats just sad, bro. "},
		{"Look, Donna, I thought that maybe we could get past this restraining order thing. I love you.", "Leave. Now. ",  "The police are on their way. ","Maybe I got you all wrong Todd."}, 
		{"You’ve reached the studio at Radio Funk Master, let us know how we can help after the tone. *BEEP*", "I want to request a song.", "I have a complaint.", "I DONT WANT TO TALK TO A MACHINE!"}, 
		{"To speak to an Operator, please press 1. To request a song, press 2. For HR, press 3. To hear the options again, press 4.", "*Press 1*", "*Press 2*", "*Press 4*"}, 
		{"Hello? How can I help you?", "Oh thank god. I’ve been trying to request a song.", "Is this catering?", "I need to talk to the HR department."}, 
		{"Transferring you to IT.", "I didnt say IT!", "No! i need an operator!", "Sweet."},
		{"This is a recorded message.", "For f&%#s sake.", "NO F*%#ING S%&T!", "*Hang up*"},
		{"Hi, you’ve reached Michael's phone, im not available, but leave a message.", "Is this some kind of joke? You need to sort your machine out.", "Who the hell is Michael?", "Hi Michael, can you please give me a call back, asap? Cheers."},
		{"This is requests! We are out of the office until Thursday. Thanks!", "No! No, Goddamnit!", "You’ve got to be kidding me.", "You’ve lost yourself a listener!"},
		{"What? I think you’re confused. Who is this?", "YOU called ME!", "I’m not confused, I got transfered to you.", "YES I AM CONFUSED! WHAT THE HELL IS GOING ON?"}, 
		{"Hey, you’re three weeks behind on rent. I NEED that money!", "Awww, s%$t man. You know things have been tight for me recently! I need more time!", "You know I’m good for it, just give me a couple more days!", "Such Payment! So Doge. Very Wealth!"}, 
		{"I dont give a s*#t, things are bad for everyone! I’ve got bills too!", "YOU DONT KNOW ME! YOU DONT KNOW WHAT IT'S LIKE!", "You think you’ve got problems? S%&t, you got it easy!", "Well I could… y’know. Give you… something"}, 
		{"Thats what you always say! You gotta do better, you’ve got to cut me in on what you’ve got going on.", "I dont have anything going on! Who told you I got s$@t goin’ on?", "Tell you what, I’ll bring you in, but you’ve gotta chill on this rent bulls*%t.", "I have a penchant for violence..."}, 
		{"What on earth are you talking about?	", "So Crypto, many value, very marketplace", "I dont even know.", "Wow! Such ignorance, missed rocket!"},
		{"Just get me my money, scumbag.	", "Yeah yeah...", "You know what? Im outta here! This place is a dump anyway!", "Do you even know who you’re talking to? You’re just a nobody, I aint giving you nothin’"}, 
		{"Oh yeah? Well... Maybe we can work out a deal, eh?", "Why dont you come in and make yourself comfortable...", "You wont regret it, you’re gunna have a real good time!", "We should have come to this arrangement years ago!"},
		{"This better be worth my time...", "Oh, you wont regret this!", "Just remember I did you this favor.", "I’ll see what I can do..."},
		{"Many thanks! wow, very crypto, how can?", "Slumdoge Millionaires!", "To the moon!", "Wow."},
		{"Hello, what seems to be the problem? It’s been a while since your last checkup.","I’m getting hairier. I think I might be a werewolf.","I keep getting these headaches…","Is my leg supposed to bend like this?"},
		{"Can I have a hair sample, please? I’ll need to do some tests, but I’m fairly sure you’re nuts.","I want a second opinion! Goodbye!","Well that all sounds reasonable, I… wait a second.","I guess the thing that bit me was just a really hairy mugger."},
		{"Have you been in an accident lately? You might be concussed.","I don’t remember. Is that bad, doctor?","Well, I was driving my car on a golf course. After that it gets kind of hazy…","No, but I have been listening to a lot of prog-industrial grindcore death thrash bands."},
		{"Oh my God you need to get to a hospital. I’m just an optometrist!","Urrgh.","Hurrghglglllffff.","Well, I could use a new pair of glasses while I’m here."},
		{"Hey, I’d like to order a large pizza, half pepperoni, half mushroom and onion.","You’ve got the wrong number. Goodbye.","Sure, do you want that pick up or delivery?","That’s all very well, but what does the pizza *mean*?"},
		{"Uh can you deliver that, please? I should be in the database, I eat an ungodly amount of pizza.","Sure, we can deliver. That’ll be $19.50 please.","I’ll tell you what, I’ll forget this conversation ever happened if you do. This isn’t a pizzeria.","First, I need you to convince me that you NEED this pizza."},
		{"Pizza is a metaphor. It represents palimpsest, the layering of ingredients and toppings over an unchanging, eternal base. In its greasiness, pizza is analogous to the slippery, ever-changing nature of meaning and identity.","I disagree. I think the mass-produced pizza is an endemic icon of total nihilism, a pale shadow of its own lack of coherence.","Buh?","Well, that was a surprisingly articulate response."},
		{"Cool. Thanks. *Click*","Ahh. Back to my thoughts.","What an idiot. I’m not running a pizza place.","Wait, maybe I AM a pizzeria. I should probably make this pie."},
		{"I was just kidding. It’s pizza.","Uh, so was I. Ha. Ha. Okayhangingupnowgoodbye.","Have you read Zizek on pizza? It’s quite pertinent to this discussion. Hey, where’d you go?","Guess what else? You’ve got the wrong number. Now who’s smart, sucker!?"}
	};

	/*       x = id
	 * b[x, 0] = link to next if answer1
	 * b[x, 1] = link to next if answer2
	 * b[x, 2] = link to next if answer3
	 */

	int [,] b = new int[,]
	{
		{1,0,4},
		{6,6,9},
		{7,8,9},
		{19,19,-1},
		{-1, -1, -1}, 
		{4, 4, 1},
		{7,7,7},
		{8,8,8},
		{9,9,10},
		{10,10,10},
		{11,12,1},
		{12, 12, -1},
		{-1, -1, -1}, 
		{12,12,-1},
		{15,16,-1},
		{-1, 12, 1},
		{-1, -1, -1}, 
		{18,19,20},
		{-1, -1, -1},
		{-1, -1, -1}, 
		{-1, -1, -1}, 
		{22,23,24},
		{25,26,27},
		{28,26,27},
		{27,25,26},
		{-1,-1,-1},
		{-1,-1,-1},
		{-1,-1,-1},
		{-1,-1,-1},
		{30,31,32},
		{33,35,34},
		{33,35,34},
		{36,33,33 },
		{-1,-1,-1},
		{-1,-1,-1},
		{-1,-1,-1},
		{-1,-1,-1},
		{38,39,40},
{-1,-1,-1},
{-1,-1,-1},
{-1,-1,-1},
{-1,42,43},
{44,-1,43},
{45,-1,-1},
{-1,-1,-1},
{-1,-1,-1}
	};
	#endregion

}