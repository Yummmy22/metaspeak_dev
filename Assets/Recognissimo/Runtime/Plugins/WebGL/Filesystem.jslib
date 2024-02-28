mergeInto(LibraryManager.library, { 
    RecognissimoUtils_Filesystem_Commit: function (callback) {
        FS.syncfs(false, () => Module.dynCall_v(callback));
    } 
});
