﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.BLL
{
    /// <summary>
    /// 目前仅提供这些语言版本的选择
    /// </summary>
    public class LanguageCodeProvider
    {
        public static readonly string cn = "cn";
        public static readonly string tw = "tw";
        public static readonly string ru = "ru";
        public static readonly string ja = "jp";
        public static readonly string fr = "fr";
        public static readonly string en = "england";
        public static readonly string de = "de";
        public static readonly string ca = "ca";
        public static readonly string hk = "hk";
        public static readonly string kr = "kr";
        public static readonly string us = "us";
        public static readonly string vn = "vn";

        public static readonly string[] LanguageCodes = new string[] { 
            "ad","ae","af","ag","ai","al","am","an","ao","ar","as","at","au","aw","ax","az","ba","bb",
            "bd","be","bf","bg","bh","bi","bj","bm","bn","bo","br","bs","bt","bv","bw","by","bz","ca",
            "catalonia","cc","cd","cf","cg","ch","ci","ck","cl","cm","cn","co","cr","cs","cu","cv","cx","cy",
            "cz","de","dj","dk","dm","do","dz","ec","ee","eg","eh","england","er","es","et","europeanunion",
            "fam","fi","fj","fk","fm","fo","fr","ga","gb","gd","ge","gf","gh","gi","gl","gm","gn","gp","gq","gr","gs",
            "gt","gu","gw","gy","hk","hm","hn","hr","ht","hu","id","ie","il","in","io","iq","ir","is","it","jm","jo","jp",
            "ke","kg","kh","ki","km","kn","kp","kr","kw","ky","kz","la","lb","lc","li","lk","lr","ls","lt","lu","lv","ly","ma",
            "mc","md","me","mg","mh","mk","ml","mm","mn","mo","mp","mq","mr","ms","mt","mu","mv","mw",
            "mx","my","mz","na","nc","ne","nf","ng","ni","nl","no","np","nr","nu","nz","om","pa","pe","pf","pg",
            "ph","pk","pl","pm","pn","pr","ps","pt","pw","py","qa","re","ro","rs","ru","rw","sa","sb","sc","scotland",
            "sd","se","sg","sh","si","sj","sk","sl","sm","sn","so","sr","st","sv","sy","sz","tc","td","tf","tg","th","tj","tk",
            "tl","tm","tn","to","tr","tt","tv","tw","tz","ua","ug","um","us","uy","uz","va","vc","ve","vg","vi","vn","vu",
            "wales","wf","ws","ye","yt","za","zm","zw"};
    }
}
