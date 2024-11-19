import { Dispatch, SetStateAction } from "react";

export const FeedbackModale = ({
  setOpenFeedbackModale,
}: {
  setOpenFeedbackModale: Dispatch<SetStateAction<boolean>>;
}) => {
  return (
    <div className="fixed z-[9998] bottom-5 right-5 max-w-[33%] shadow-xl">
      <div className="relative z-[9999] bg-white rounded p-3 border">
        {/* HEAD */}
        <div className="mb-2 flex justify-between">
          <h3 className="text-xl text-secondary_rose">
            ❤️ Aidez nous à nous améliorer !
          </h3>
          <button
            className="h-8 w-8 bg-secondary_rose text-white rounded"
            onClick={() => setOpenFeedbackModale(false)}
          >
            X
          </button>
        </div>
        {/* BODY */}
        <div className="flex flex-col gap-3">
          <p>
            Votre avis nous intéresse afin d'améliorer continuellement notre
            service GoodFood. <strong>C'est gratuit et anonyme !</strong>
          </p>

          <label htmlFor="note">Note*</label>
          <input type="text" className="border rounded" id="note"></input>

          <label htmlFor="comment">Commentaire*</label>
          <input type="texte" id="comment" className="border rounded"></input>

          <button className="bg-black text-white rounded h-8 ">Envoyer</button>
        </div>
      </div>
      <div className="absolute top-1 left-1 bg-black w-full h-full rounded"></div>
    </div>
  );
};
