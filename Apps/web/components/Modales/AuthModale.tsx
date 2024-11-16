import { Dispatch, SetStateAction, useEffect, useState } from "react";

type User = {
  email: string;
  password: string;
  confirmationPassword: string;
};

export const AuthModale = ({
  setAuthModale,
  authUserChoice,
  setAuthUserChoice,
}: {
  setAuthModale: Dispatch<SetStateAction<boolean>>;
  authUserChoice: String;
  setAuthUserChoice: Dispatch<SetStateAction<string>>;
}) => {
  const [user, setUser] = useState<Partial<User>>({});

  useEffect(() => {
    console.log("user : ", user);
  }, [user]);

  return (
    <div className="flex z-100 top-0 left-0 w-screen h-screen justify-center items-center">
      <div
        className="absolute bg-black/40 w-full h-full z-0"
        onClick={() => {
          setAuthModale(false), setUser({});
        }}
      ></div>

      <div className="relative bg-white w-1/3 h-auto z-1 rounded-lg p-3">
        {/* HEAD */}
        <div className="mb-5">
          <div className="relative w-fit">
            <h1 className="relative z-10">
              {authUserChoice == "SignIn" ? "CONNEXION" : "INSCRIPTION"}
            </h1>
            <div className="absolute z-0 top-7 left-7 h-[20px] w-full bg-secondary_green" />
          </div>
        </div>
        <div className="h-px w-full bg-black" />

        {/* BODY */}

        <p>Erreur : [message]</p>

        {authUserChoice == "SignIn" ? (
          <div className="my-6">
            <form className="flex flex-col gap-3">
              <div className="flex flex-col">
                <label htmlFor="email">E-mail</label>
                <input
                  id="email"
                  type="text"
                  className="border rounded"
                  required
                ></input>
              </div>
              <div className="flex flex-col">
                <label htmlFor="password">Mot de passe</label>
                <input
                  type="password"
                  id="password"
                  className="border rounded"
                  required
                />
              </div>
              <button type="submit" className="bg-black text-white rounded">
                Se connecter
              </button>
            </form>
          </div>
        ) : (
          <div className="my-6">
            <form className="flex flex-col gap-3">
              <div className="flex flex-col">
                <label htmlFor="email">E-mail</label>
                <input
                  type="text"
                  id="email"
                  className="border rounded"
                  required
                  onChange={(e) => setUser({ ...user, email: e.target.value })}
                ></input>
              </div>

              <div className="flex flex-col">
                <label htmlFor="password">Mot de passe</label>
                <input
                  type="password"
                  id="password"
                  className="border rounded"
                  required
                  onChange={(e) =>
                    setUser({ ...user, password: e.target.value })
                  }
                ></input>
              </div>

              <div className="flex flex-col">
                <label htmlFor="passwordConfirmation">
                  Confirmation du mot de passe
                </label>
                <input
                  type="password"
                  id="passwordConfirmation"
                  className="border rounded"
                  required
                  onChange={(e) =>
                    setUser({ ...user, confirmationPassword: e.target.value })
                  }
                ></input>
              </div>
              <button type="submit" className="bg-black text-white rounded">
                S'enregistrer
              </button>
            </form>
          </div>
        )}

        <div className="h-px w-full bg-black" />

        {/* FOOTER */}
        <div className="my-2">
          <p
            className="hover:underline cursor-pointer"
            onClick={() => {
              authUserChoice == "SignIn"
                ? (setAuthUserChoice("SignUp"), setUser({}))
                : (setAuthUserChoice("SignIn"), setUser({}));
            }}
          >
            {authUserChoice == "SignIn"
              ? "Pas de compte ?"
              : "Déjà un compte ?"}
          </p>
        </div>
      </div>
    </div>
  );
};
