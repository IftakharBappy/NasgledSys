using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.EM
{
    public static class EM_AreaNote
    {
        public static AreaNoteViewModel ConvertToModel(AreaNote ent)
        {
            AreaNoteViewModel bo = new AreaNoteViewModel();

            bo.NoteKey = ent.NoteKey;
            bo.AreaKey = ent.AreaKey;
            bo.NoteContent = ent.NoteContent;
            bo.Internal = ent.Internal;

            return bo;
        }

        public static AreaNote ConvertToEntity(AreaNoteViewModel bo)
        {
            AreaNote ent = new AreaNote();
            ent.NoteKey = bo.NoteKey;
            ent.AreaKey = bo.AreaKey;
            ent.NoteContent = bo.NoteContent;
            ent.Internal = bo.Internal;

            return ent;
        }
    }
}