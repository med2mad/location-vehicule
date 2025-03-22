document.addEventListener("DOMContentLoaded", function () {
    gsap.registerPlugin(ScrollTrigger);

    gsap.to(".hero", {
        opacity: 1,
        y: 0, // Moves to original position
        duration: 1.5,
        ease: "power3.out",
        scrollTrigger: {
            trigger: ".hero",
            start: "top 80%", // Triggers when the top of the image is 80% in view
            toggleActions: "play none none none",
        }
    });

    gsap.to(".vehicule", {
        opacity: 1,
        x: 0, // Move to original position
        duration: 1,
        ease: "none",
        stagger: 0.3, // Delay between elements
        scrollTrigger: {
            trigger: ".vehicule",
            start: "top 100%", // Triggers when top of div is 100% in view
            toggleActions: "play none none none",
        }
    });
});