using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookShelf : MonoBehaviour {
    [SerializeField] Books[] books;
    [SerializeField] HumanController humanController;
    bool bMessed = false;

    private void Start() {
        InvokeRepeating("CheckByRate", 0.0f, 0.2f);
    }

    void CheckByRate() {
        if (!humanController.bookNeedsFix && bMessed) {
            CleanUp();
            bMessed = false;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("ToyCannonBall")) {
            MessUp();
            bMessed = true;
        }
    }

    public void MessUp() {
        foreach (Books book in books) {
            book.Trigger(true);
        }
    }

    public void CleanUp() {
        foreach (Books book in books) {
            book.Trigger(false);
        }
    }
}