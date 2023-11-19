import HeroImage from "@/components/HeroImage";
import SearchForm from "@/components/SearchForm";

export default function Home() {
  return (
    <main className="flex min-h-screen flex-col mb-40">
      <HeroImage />
      <div className="w-100 mx-auto px-6 sm:max-w-2xl md:max-w-3xl md:px-12 lg:max-w-5xl xl:max-w-7xl xl:px-32">
        <div className="text-center">
          <div className="block rounded-lg bg-[hsla(0,0%,100%,0.8)] px-6 py-12 shadow-[0_2px_15px_-3px_rgba(0,0,0,0.07),0_10px_20px_-2px_rgba(0,0,0,0.04)] dark:bg-[hsla(0,0%,15%,0.8)] dark:shadow-black/20 md:py-16 md:px-12">
            <h1 className="mt-6 mb-16 text-5xl font-bold tracking-tight md:text-6xl xl:text-7xl">
              Find your dream Car <br />
              <span className="text-primary">for your Trip!</span>
            </h1>
            <SearchForm />
          </div>
        </div>
      </div>
    </main>
  );
}
